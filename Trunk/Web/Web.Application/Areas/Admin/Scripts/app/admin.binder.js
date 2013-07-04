define('admin.binder',
    ['jquery', 'knockback', 'logger'],
    function ($, kb, logger) {
        var bind = function () {
                logger.log('admin binding bound');
            };

        return {
            bind: bind
        };
    });