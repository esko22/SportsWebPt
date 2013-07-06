﻿define('router', ['backbone', 'jquery', 'presenter','vm.admin.equipment', 'vm.admin.videos'],
    function (backbone, $, presenter, EquipmentVM, VideoVM) {

        var configure = function () {
            var mainRouter = backbone.Router.extend({
                routes: {
                    '': 'nav',
                    'videos': 'videos',
                    'exercises': 'exercises',
                    'equipment': 'equipment',
                    '*actions': 'defaultRoute'
                }
            });

            var router = new mainRouter();

            router.on('route:defaultRoute', function () {
            });

            router.on('route:nav', function () {
                $('.view').hide();
                $('.active').removeClass('active');
                presenter.transitionTo($('#admin-nav-panel'),'','');
            });

            router.on('route:videos', function () {
                $('.view').hide();
                $('.active').removeClass('active');
                kb.applyBindings(VideoVM, $('#admin-video-panel').get(0));
                presenter.transitionTo($('#admin-video-panel'), '', '');
            });

            router.on('route:exercises', function () {
                $('.view').hide();
                $('.active').removeClass('active');
                presenter.transitionTo($('#admin-exercise-panel'), '', '');
            });

            router.on('route:equipment', function () {
                $('.view').hide();
                $('.active').removeClass('active');
                kb.applyBindings(EquipmentVM, $('#admin-equipment-panel').get(0));
                presenter.transitionTo($('#admin-equipment-panel'), '', '');
            });

 
            backbone.history.start();
        };

        return {
            configure: configure
        };

    });