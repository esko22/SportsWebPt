define('shared.binder',
    ['jquery', 'ko', 'config', 'shared.vm', 'logger'],
    function ($, ko, config, vm, logger) {
        var
            ids = config.viewIds,

            bind = function () {
                logger.log('shared binding bound');
                
                ko.applyBindings(vm.header, getView(ids.header));
                ko.applyBindings(vm.footer, getView(ids.footer));
                ko.applyBindings(vm.login, getView(ids.login));
            },

            getView = function (viewName) {
                return $(viewName).get(0);
            };

        return {
            bind: bind
        };
    });