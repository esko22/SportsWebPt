define('bootstrapper', ['jquery', 'logger','config'],
    function($, logger, config) {
        var run = function() {
            logger.log('running base bs');
        };

        return { run: run };
    });