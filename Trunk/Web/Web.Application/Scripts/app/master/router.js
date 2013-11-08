define('router', ['backbone', 'presenter', 'config', 'vm.examine.page', 'vm.splash'],
    function (backbone, presenter, config, examinePage, splashPage) {

        var configure = function() {
            var mainRouter = backbone.Router.extend({
                routes: {
                    '': 'main',
                    'examine': 'examine',
                    'examine/detail': 'examineDetail',
                    'examine/report': 'examineReport',
                    '*actions': 'defaultRoute'
                }
            });

            var router = new mainRouter();

            router.on('route:defaultRoute', function() {
            });

            router.on('route:examine', function () {
                splashPage.isVisible(false);
                examinePage.onVisible();
                transitionTo(config.viewIds.examineSkeleton);
                $('#skeleton-nav').addClass('active');
            });

            router.on('route:examineDetail', function () {
                examinePage.showDetail();
                transitionTo(config.viewIds.examineDetail);
            });

            router.on('route:examineReport', function () {
                examinePage.showReport();
                transitionTo(config.viewIds.examineReport);
            });

            router.on('route:main', function () {
                examinePage.isVisible(false);
                splashPage.isVisible(true);
            });

            backbone.history.start();
        };
            
        function transitionTo(viewId) {
            presenter.transitionTo($(viewId), '', '');
        }

        
        return {
            configure: configure
        };

    });