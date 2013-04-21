define('router', ['backbone','jquery','presenter', 'vm.examine.container'],
    function (backbone,$, presenter, container) {

        var configure = function () {
            var mainRouter = backbone.Router.extend({
                routes: {
                    '': 'skeleton',
                    'describe': 'description'
                }
            });

            var router = new mainRouter();

            router.on('route:description', function () {
                $('.view').hide();
                $('.active').removeClass('active');
                presenter.transitionTo(
                    $('#description-detail'),
                    '',
                    '');
                $('#description-nav').addClass('active');
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