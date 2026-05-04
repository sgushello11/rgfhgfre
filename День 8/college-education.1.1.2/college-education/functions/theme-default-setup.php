<?php
/*
 * college-education Main Sidebar
 */
function college_education_widgets_init() {
    register_sidebar(array(
        'name' => __('Main Sidebar', 'college-education'),
        'id' => 'sidebar-1',
        'description' => __('Main sidebar that appears on the right.', 'college-education'),
        'before_widget' => '<aside id="%1$s" class="menu-left widget widget_recent_entries %2$s">',
        'after_widget' => '</aside>',
        'before_title' => '<h6 class="widget-title-sidebar">',
        'after_title' => '</h6>',
    ));
    register_sidebar(array(
        'name' => __('Footer 1', 'college-education'),
        'id' => 'footer-1',
        'description' => __('Footer that appears on the down.', 'college-education'),
        'before_widget' => '<aside id="%1$s" class="footer-widget widget widget_recent_entries %2$s">',
        'after_widget' => '</aside>',
        'before_title' => '<h6>',
        'after_title' => '</h6>',
    ));
    register_sidebar(array(
        'name' => __('Footer 2', 'college-education'),
        'id' => 'footer-2',
        'description' => __('Footer that appears on the down.', 'college-education'),
        'before_widget' => '<aside id="%1$s" class="footer-widget widget widget_recent_entries %2$s">',
        'after_widget' => '</aside>',
        'before_title' => '<h6>',
        'after_title' => '</h6>',
    ));
    register_sidebar(array(
        'name' => __('Footer 3', 'college-education'),
        'id' => 'footer-3',
        'description' => __('Footer that appears on the down.', 'college-education'),
        'before_widget' => '<aside id="%1$s" class="footer-widget widget widget_recent_entries %2$s">',
        'after_widget' => '</aside>',
        'before_title' => '<h6>',
        'after_title' => '</h6>',
    ));
    register_sidebar(array(
        'name' => __('Footer 4', 'college-education'),
        'id' => 'footer-4',
        'description' => __('Footer that appears on the down.', 'college-education'),
        'before_widget' => '<aside id="%1$s" class="footer-widget widget widget_recent_entries %2$s">',
        'after_widget' => '</aside>',
        'before_title' => '<h6>',
        'after_title' => '</h6>',
    ));
}
add_action('widgets_init', 'college_education_widgets_init');
 
function college_education_excerpt( $limit) {
        if ( is_admin() ) {
                return $limit;
        }
        return get_theme_mod('blogPostExcerptTextLimit',20);
}
add_filter( 'excerpt_length', 'college_education_excerpt',999);

function college_education_font_families_filter($font_families) {
    $font_families['Poppins'] = 'Poppins';
    return $font_families;
}
add_filter('siteorigin_widgets_font_families', 'college_education_font_families_filter');

/*
* Function For Tag Meta List
*/
function college_education_tag_meta() {  
	$CollegeEducationTagList = get_the_tag_list('', esc_html__( ', #', 'college-education' ));
	if(!empty($CollegeEducationTagList)) { ?>
	<div class="single-blog-tag"><span ><?php esc_html_e('Tag :','college-education'); ?></span><?php echo '#'.get_the_tag_list('', esc_html__( ', #', 'college-education' )); ?></div>  
	<?php } ?>  
  <div class="single-blog-author"><i class="fa fa-user"></i>&nbsp;&nbsp;<?php echo esc_html(get_the_author()); ?></div>
  <div class="single-blog-date"><i class="fa fa-calendar"></i>&nbsp;&nbsp;<?php echo esc_html(get_the_date()); ?></div>  
  <?php 
}


/*
* TGM plugin activation register hook 
*/
add_action( 'tgmpa_register', 'college_education_register_required_plugins' );
function college_education_register_required_plugins() {
    $plugins = array(
      array(
         'name'      => __('Page Builder by SiteOrigin','college-education'),
         'slug'      => 'siteorigin-panels',
         'required'  => false,
      ),
      array(
         'name'      => __('SiteOrigin Widgets Bundle','college-education'),
         'slug'      => 'so-widgets-bundle',
         'required'  => false,
      ), 
      array(
         'name'      => __('Contact Form 7','college-education'),
         'slug'      => 'contact-form-7',
         'required'  => false,
      ),   
    );
    $config = array(
      'id'           => 'college-education',
      'default_path' => '',
      'menu'         => 'tgmpa-install-plugins',
      'has_notices'  => true,
      'dismissable'  => true,
      'dismiss_msg'  => '',
      'is_automatic' => false,
      'message'      => '',
      'strings'      => array(
         'page_title'                      => __( 'Install Recommended Plugins', 'college-education' ),
         'menu_title'                      => __( 'Install Plugins', 'college-education' ),
         'installing'                      => /* translators: %s plugins name */__(  'Installing Plugin: %s', 'college-education' ), 
         'oops'                            => __( 'Something went wrong with the plugin API.', 'college-education' ),
         
         'complete'                        => /* translators: %s plugins name */__( 'All plugins installed and activated successfully. %s', 'college-education' ), 
         'nag_type'                        => 'updated'
      )
    );
    tgmpa( $plugins, $config );
}
