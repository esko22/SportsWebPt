﻿define('vm.research.page',
    ['vm.research.exercise.listing', 'vm.research.injury.listing', 'vm.research.locate','vm.research.plan.listing','vm.research.nav', 'ko', 'vm.research.injury.detail'],
    function(exercisePane, injuryPane, locatePane, planPane, navPane, ko, injuryDetailPane) {

        var isVisible = ko.observable(false);

        function onVisible() {
            isVisible(true);
        }
        
        function showInjuries() {
            injuryPane.init();
        }
        
        function showPlans() {
            planPane.init();
        }
        
        function showeExercises() {
            exercisePane.init();
        }
        
        function showLocations() {
            locatePane.init();
        }
        
        function showInjuryDetail(searchKey) {
            injuryDetailPane.init(searchKey);
        }

        return {
            isVisible : isVisible,
            onVisible : onVisible,
            navPane: navPane,
            exercisePane: exercisePane,
            injuryPane: injuryPane,
            locatePane: locatePane,
            planPane: planPane,
            showInjuries: showInjuries,
            showPlans: showPlans,
            showExercises: showeExercises,
            showLocations: showLocations,
            injuryDetailPane: injuryDetailPane,
            showInjuryDetail: showInjuryDetail
        };
    });