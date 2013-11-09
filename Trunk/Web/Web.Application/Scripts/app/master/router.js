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
                if (!examinePage.isVisible()) {
                    splashPage.isVisible(false);
                    examinePage.onVisible();
                }
                examinePage.showSkeleton();
                transitionTo(config.viewIds.examineSkeleton);
            });

            router.on('route:examineDetail', function () {
                if (examinePage.canShowDetail()) {
                    examinePage.showDetail();
                    transitionTo(config.viewIds.examineDetail);
                } else {
                    window.location.hash = 'examine';
                }
            });

            router.on('route:examineReport', function () {
                if(examinePage.canShowReport()){
                    examinePage.showReport();
                    transitionTo(config.viewIds.examineReport);
                } else {
                    window.location.hash = 'examine';
                }
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