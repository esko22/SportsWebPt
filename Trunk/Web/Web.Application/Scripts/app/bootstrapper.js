define('bootstrapper', ['jquery', 'logger','config'],
    function($, logger, config) {
        var run = function() {
            logger.log('running base bs');
            config.someval = 'test';
        };

        return { run: run };
    });