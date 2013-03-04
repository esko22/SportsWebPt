define('shared.bootstrapper', ['ko','logger', 'shared.binder'],
    function (ko, logger, binder) {
        var run = function () {
            logger.log('running shared bs');
            binder.bind();
        };

        return { run: run };
    });