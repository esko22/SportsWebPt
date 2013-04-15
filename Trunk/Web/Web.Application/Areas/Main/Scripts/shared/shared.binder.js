define('shared.binder',
    ['jquery', 'ko', 'config', 'shared.vm', 'logger'],
    function ($, ko, config, vm, logger) {
        var ids = config.viewIds,
            bind = function() {
                logger.log('shared binding bound');

                ko.applyBindings(vm.header, $(ids.header).get(0));
                ko.applyBindings(vm.footer, $(ids.footer).get(0));
                ko.applyBindings(vm.login, $(ids.login).get(0));
            };


        return {
            bind: bind
        };
    });