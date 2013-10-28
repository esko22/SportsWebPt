define('bootstrapper', ['ko', 'logger', 'admin.binder'],
    function (ko, logger, binder) {
        var run = function () {
            logger.log('running admin area bs');
            binder.bind();
        };

        return { run: run };
    });