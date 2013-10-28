define('shared.binder',
    ['jquery', 'knockback', 'config', 'shared.vm', 'logger'],
    function ($, kb, config, vm, logger) {
        var ids = config.viewIds,
            bind = function() {
                logger.log('shared binding bound');

                kb.applyBindings(vm.header, $(ids.header).get(0));
                kb.applyBindings(vm.footer, $(ids.footer).get(0));
                kb.applyBindings(vm.login, $(ids.login).get(0));
            };


        return {
            bind: bind
        };
    });