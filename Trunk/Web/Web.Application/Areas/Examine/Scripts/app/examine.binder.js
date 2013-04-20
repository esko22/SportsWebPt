define('examine.binder',
    ['jquery', 'knockback', 'examine.vm', 'logger'],
    function ($, kb, vm, logger) {
        var ids = vm.viewIds,
            bind = function () {
                logger.log('examine binding bound');

                kb.applyBindings(vm.container, $(ids.container).get(0));
                kb.applyBindings(vm.skeleton, $(ids.skeleton).get(0));
                kb.applyBindings(vm.discomfort, $(ids.discomfort).get(0));
            };


        return {
            bind: bind
        };
    });