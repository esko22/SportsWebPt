define('research.binder',
    ['jquery', 'knockback', 'logger'],
    function ($, kb, logger) {
        var bind = function () {
                logger.log('research binding bound');
            };

        return {
            bind: bind
        };
    });