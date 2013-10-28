define('bootstrapper', ['jquery', 'logger','config', 'knockback', 'vm.main'],
    function($, logger, config, kb, vmMain) {
        var run = function() {
            logger.log('running base bs');
            
            kb.applyBindings(vmMain, $(config.viewIds.masterContainer).get(0));
        };

        return { run: run };
    });