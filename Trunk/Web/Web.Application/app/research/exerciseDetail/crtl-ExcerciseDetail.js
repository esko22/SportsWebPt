'use strict';

swptApp.controller('ExerciseDetailController',
    function ExerciseDetailController($scope, $location, exerciseDetailService) {

        $scope.exercise = exerciseDetailService.getExercise('standing-soleus-strech-1');
        
    })