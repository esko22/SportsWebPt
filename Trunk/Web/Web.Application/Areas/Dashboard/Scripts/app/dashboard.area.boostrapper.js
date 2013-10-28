define('bootstrapper', ['ko', 'logger','dashboard.binder'],
    function (ko, logger, binder) {
        var run = function () {
            logger.log('running dashboard area bs');
            binder.bind();
        };

        return { run: run };
    });