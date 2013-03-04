define('sharedBootstrapper', ['ko','logger'],
    function (ko, logger) {
        var run = function () {
            logger.log('running shared bs');
        };

        return { run: run };
    });