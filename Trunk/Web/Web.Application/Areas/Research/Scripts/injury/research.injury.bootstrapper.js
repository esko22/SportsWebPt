﻿define('area.bootstrapper', ['knockback', 'logger', 'vm.research.injury', 'jquery'],
    function (kb, logger, vmInjury ,$) {
        var run = function () {
            logger.log('running research plan bs');

            kb.applyBindings(vmInjury, $('#research-injury-page').get(0));
         };

        return { run: run };
    });


define('router', ['ko', 'logger'],
    function (ko, logger) {
        var configure = function () {
        };

        return { configure: configure };
    });