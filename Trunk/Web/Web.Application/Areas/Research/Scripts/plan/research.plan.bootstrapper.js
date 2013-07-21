define('area.bootstrapper', ['knockback', 'logger', 'vm.research.plan', 'jquery'],
    function (kb, logger, vmPlan ,$) {
        var run = function () {
            logger.log('running research plan bs');

            kb.applyBindings(vmPlan, $('#research-plan-page').get(0));
         };

        return { run: run };
    });


define('router', ['ko', 'logger'],
    function (ko, logger) {
        var configure = function () {
        };

        return { configure: configure };
    });