define('area.bootstrapper', ['knockback', 'logger', 'vm.research.exercise', 'jquery'],
    function (kb, logger, vmExercise ,$) {
        var run = function () {
            logger.log('running research exercise bs');

            kb.applyBindings(vmExercise, $('#research-exercise-page').get(0));
         };

        return { run: run };
    });


define('router', ['ko', 'logger'],
    function (ko, logger) {
        var configure = function () {
        };

        return { configure: configure };
    });