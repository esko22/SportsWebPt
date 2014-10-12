'use strict';

jQueryPluginModule.directive('flexSlider', [function () {
    return {
        restrict: 'A',
        link: function ($scope, element) {
            $(element).each(function () {
                var sliderSettings = {
                    animation: $(this).attr('data-transition'),
                    selector: ".slides > .slide",
                    controlNav: true,
                    smoothHeight: true,
                    start: function (slider) {
                        //hide all animated elements
                        slider.find('[data-animate-in]').each(function () {
                            $(this).css('visibility', 'hidden');
                        });

                        //slide backgrounds
                        slider.find('.slide-bg').each(function () {
                            $(this).css({ 'background-image': 'url(' + $(this).data('bg-img') + ')' });
                            $(this).css('visibility', 'visible').addClass('animated').addClass($(this).data('animate-in'));
                        });

                        //animate in first slide
                        slider.find('.slide').eq(1).find('[data-animate-in]').each(function () {
                            $(this).css('visibility', 'hidden');
                            if ($(this).data('animate-delay')) {
                                $(this).addClass($(this).data('animate-delay'));
                            }
                            if ($(this).data('animate-duration')) {
                                $(this).addClass($(this).data('animate-duration'));
                            }
                            $(this).css('visibility', 'visible').addClass('animated').addClass($(this).data('animate-in'));
                            $(this).one('webkitAnimationEnd oanimationend msAnimationEnd animationend',
                              function () {
                                  $(this).removeClass($(this).data('animate-in'));
                              }
                            );
                        });
                    },
                    before: function (slider) {
                        slider.find('.slide-bg').each(function () {
                            $(this).removeClass($(this).data('animate-in')).removeClass('animated').css('visibility', 'hidden');
                        });

                        //hide next animate element so it can animate in
                        slider.find('.slide').eq(slider.animatingTo + 1).find('[data-animate-in]').each(function () {
                            $(this).css('visibility', 'hidden');
                        });
                    },
                    after: function (slider) {
                        //alert(slider.currentSlide);
                        //hide animtaed elements so they can animate in again
                        slider.find('.slide').find('[data-animate-in]').each(function () {
                            $(this).css('visibility', 'hidden');
                        });

                        //animate in next slide
                        slider.find('.slide').eq(slider.animatingTo + 1).find('[data-animate-in]').each(function () {
                            if ($(this).data('animate-delay')) {
                                $(this).addClass($(this).data('animate-delay'));
                            }
                            if ($(this).data('animate-duration')) {
                                $(this).addClass($(this).data('animate-duration'));
                            }
                            $(this).css('visibility', 'visible').addClass('animated').addClass($(this).data('animate-in'));
                            $(this).one('webkitAnimationEnd oanimationend msAnimationEnd animationend',
                              function () {
                                  $(this).removeClass($(this).data('animate-in'));
                              }
                            );
                        });
                    }
                };

                var sliderNav = $(this).attr('data-slidernav');
                if (sliderNav !== 'auto') {
                    sliderSettings = $.extend({}, sliderSettings, {
                        manualControls: sliderNav + ' li a',
                        controlsContainer: '.flexslider-wrapper'
                    });
                }

                $('html').addClass('has-flexslider');
                $(this).flexslider(sliderSettings);
            });

            $(element).resize(); //make sure height is right load assets loaded

        }
    };
}]);