define('dashboard.binder',
    ['jquery', 'knockback', 'vm.dashboard.page', 'logger'],
    function ($, kb, dashboard, logger) {
        var bind = function () {
                logger.log('dashboard binding bound');

                kb.applyBindings(dashboard, $('#dashboard-container').get(0));
            };

        return {
            bind: bind
        };
    });