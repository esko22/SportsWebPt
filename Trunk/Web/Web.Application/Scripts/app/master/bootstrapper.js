define('bootstrapper', ['jquery', 'logger','config', 'knockback', 'vm.master.page'],
    function($, logger, config, kb, vmMasterPage) {
        var run = function() {
            logger.log('running base bs');
            
            $(document).ajaxError(function (event, jqXhr, settings, error) {
                switch (jqXhr.status) {
                    case 500:
                        config.notifier.clear();
                        config.notifier.error(jqXhr.responseJSON.message, error, { extendedTimeOut: 0, timeOut: 0 });
                        break;
                    case 401:
                        // only handle the case where the session is timed out on the server but the client is not aware yet.
                        // otherwise, it is being handled in the router.
                        //if (config.currentUser.isAuthenticated()) {
                        //    window.location = '/logout';
                        //}
                        break;
                }
            });

            kb.applyBindings(vmMasterPage, $(config.viewIds.masterContainer).get(0));
        };

        return { run: run };
    });