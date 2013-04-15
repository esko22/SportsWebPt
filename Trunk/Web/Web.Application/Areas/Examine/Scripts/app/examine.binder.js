define('examine.binder',
    ['jquery', 'ko', 'examine.vm', 'logger'],
    function ($, ko, vm, logger) {
        var ids = vm.viewIds,
            bind = function () {
                logger.log('examine binding bound');

                ko.applyBindings(vm.skeletor, $(ids.skeletor).get(0));
            };


        return {
            bind: bind
        };
    });