define('auth.form.bootstrapper', ['jquery', 'logger', 'config','shared.vm','knockback'],
    function ($, logger, config, vm, kb) {
        var run = function () {
            logger.log('running auth form bs');

            var ids = config.viewIds;
            kb.applyBindings(vm.login, getView(ids.login));
        },

        getView = function (viewName) {
            return $(viewName).get(0);
        };


        return { run: run };
    });