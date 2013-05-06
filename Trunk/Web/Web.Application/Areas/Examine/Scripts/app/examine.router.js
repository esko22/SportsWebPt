define('router', ['backbone','jquery','presenter', 'vm.examine.container', 'vm.examine.detail'],
    function (backbone,$, presenter, container, detail) {

        var configure = function () {
            var mainRouter = backbone.Router.extend({
                routes: {
                    '': 'skeleton',
                    'detail': 'detail',
                    'injuries/:id' : 'injuries'
                }
            });

            var router = new mainRouter();

            router.on('route:detail', function () {
                $('.view').hide();
                $('.active').removeClass('active');
                detail.bindSelectedAreas(container.selectedAreas());
                presenter.transitionTo(
                    $('#examine-detail'),
                    '',
                    '');
                $('#detail-nav').addClass('active');
            });
            router.on('route:skeleton', function () {
                $('.view').hide();
                $('.active').removeClass('active');
                presenter.transitionTo(
                    $('#examine-skeleton'),
                    '',
                    '');
                $('#skeleton-nav').addClass('active');
            });
            router.on('route:injuries', function (id) {
                $('.view').hide();
                $('.active').removeClass('active');
                alert(id);
                presenter.transitionTo(
                    $('#examine-injuries'),
                    '',
                    '');
                $('#injuries-nav').addClass('active');
            });

            backbone.history.start();
        };

        return {
            configure: configure
        };

    });