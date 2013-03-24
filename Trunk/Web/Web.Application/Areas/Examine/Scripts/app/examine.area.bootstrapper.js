define('area.bootstrapper', ['ko', 'logger'],
    function (ko, logger) {
        var run = function () {
            logger.log('running examine area bs');
        };

        return { run: run };
    });