define('bootstrapper', ['jquery', 'logger'],
    function($, logger) {
        var run = function() {
            logger.log('running base bs');
        };

        return { run: run };
    });