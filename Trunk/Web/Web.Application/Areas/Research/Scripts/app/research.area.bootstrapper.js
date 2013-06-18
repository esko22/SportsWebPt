define('area.bootstrapper', ['ko', 'logger', 'research.binder'],
    function (ko, logger, binder) {
        var run = function () {
            logger.log('running research area bs');
            binder.bind();
        };

        return { run: run };
    });