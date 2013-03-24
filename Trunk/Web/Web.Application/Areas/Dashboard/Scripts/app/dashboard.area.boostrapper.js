define('area.bootstrapper', ['ko', 'logger'],
    function (ko, logger) {
        var run = function () {
            logger.log('running dashboard area bs');
        };

        return { run: run };
    });