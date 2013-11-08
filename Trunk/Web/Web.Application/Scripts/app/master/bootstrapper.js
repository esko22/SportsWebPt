define('bootstrapper', ['jquery', 'logger','config', 'knockback', 'vm.master.page'],
    function($, logger, config, kb, vmMasterPage) {
        var run = function() {
            logger.log('running base bs');
            
            kb.applyBindings(vmMasterPage, $(config.viewIds.masterContainer).get(0));
        };

        return { run: run };
    });