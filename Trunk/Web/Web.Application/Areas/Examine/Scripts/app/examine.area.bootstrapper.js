define('area.bootstrapper', ['ko', 'logger','examine.binder'],
    function (ko, logger, binder) {
        var run = function () {
            logger.log('running examine area bs');
            binder.bind();
        };

        return { run: run };
    });