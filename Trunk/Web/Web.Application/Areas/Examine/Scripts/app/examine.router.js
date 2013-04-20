define('router', ['backbone','jquery','presenter', 'vm.examine.container'],
    function (backbone,$, presenter, container) {

        var configure = function () {
            var mainRouter = backbone.Router.extend({
                routes: {
                    '': 'skeleton',
                    'discomfort': 'discomfort'
                }
            });

            var router = new mainRouter();

            router.on('route:discomfort', function () {
                $('.view').hide();
                $('.active').removeClass('active');
                presenter.transitionTo(
                    $('#discomfort-detail'),
                    '',
                    '');
                $('#discomfort-nav').addClass('active');
            });
            router.on('route:skeleton', function () {
                $('.view').hide();
                $('.active').removeClass('active');
                presenter.transitionTo(
                    $('#skeleton-container'),
                    '',
                    '');
                $('#skeleton-nav').addClass('active');
            });

            backbone.history.start();
        };

        return {
            configure: configure
        };

    });