define('router', ['backbone','jquery','presenter', 'vm.examine.container', 'vm.examine.detail', 'model.diagnosis.report', 'vm.examine.report'],
    function (backbone,$, presenter, container, detail, DiagnosisReport, report) {

        var configure = function () {
            var mainRouter = backbone.Router.extend({
                routes: {
                    '': 'skeleton',
                    'detail': 'detail',
                    'report/:id' : 'report'
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
            router.on('route:report', function (id) {
                $('.view').hide();
                $('.active').removeClass('active');

                var diagnosisReport = new DiagnosisReport({ diffDiagId: id });
                diagnosisReport.fetch({
                    success: function() {
                        report.bindReport(diagnosisReport);
                        presenter.transitionTo($('#examine-report'),'','');
                        $('#report-nav').addClass('active');
                        setTimeout(function() {
                            sublime.load();
                        }, 3000);
                        setTimeout(function () {
                            sublime.load();
                        }, 12000);

                    }
                });

            });

            backbone.history.start();
        };

        return {
            configure: configure
        };

    });