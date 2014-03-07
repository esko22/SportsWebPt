'use strict';

swptApp.directive('jpanel', function () {
    return {
        restrict: 'A',
        link: function ($scope, element) {
            //Plugin: jPanel Menu
            // data-toggle=jpanel-menu must be present on .navbar-btn
            // @todo - allow options to be passed via data- attr
            // --------------------------------
            if ($.jPanelMenu && $('[data-toggle=jpanel-menu]').size() > 0) {
                var jpanelMenuTrigger = $('[data-toggle=jpanel-menu]');

                var jPM = $.jPanelMenu({
                    menu: jpanelMenuTrigger.data('target'),
                    direction: 'left',
                    trigger: '.' + jpanelMenuTrigger.attr('class'),
                    excludedPanelContent: '.jpanel-menu-exclude',
                    openPosition: '280px',
                    afterOpen: function () {
                        jpanelMenuTrigger.addClass('open');
                        $('html').addClass('jpanel-menu-open');
                    },
                    afterClose: function () {
                        jpanelMenuTrigger.removeClass('open');
                        $('html').removeClass('jpanel-menu-open');
                    }
                });

                //jRespond settings
                var jRes = jRespond([
                  {
                      label: 'small',
                      enter: 0,
                      exit: 1010
                  }
                ]);

                //turn jPanel Menu on/off as needed
                jRes.addFunc({
                    breakpoint: 'small',
                    enter: function () {
                        jPM.on();
                    },
                    exit: function () {
                        jPM.off();
                    }
                });
            }
        }
    };
});



