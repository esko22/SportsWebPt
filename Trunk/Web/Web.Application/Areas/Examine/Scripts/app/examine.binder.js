define('examine.binder',
    ['jquery', 'knockback', 'examine.vm', 'logger'],
    function ($, kb, vm, logger) {
        var ids = vm.viewIds,
            bind = function () {
                logger.log('examine binding bound');

                kb.applyBindings(vm.skeletor, $(ids.skeletor).get(0));
            };


        return {
            bind: bind
        };
    });