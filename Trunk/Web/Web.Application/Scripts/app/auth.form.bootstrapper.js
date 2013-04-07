define('auth.form.bootstrapper', ['jquery', 'logger', 'config','shared.vm'],
    function ($, logger, config, vm) {
        var run = function () {
            logger.log('running auth form bs');

            var ids = config.viewIds;
            ko.applyBindings(vm.login, getView(ids.login));
        },

        getView = function (viewName) {
            return $(viewName).get(0);
        };


        return { run: run };
    });