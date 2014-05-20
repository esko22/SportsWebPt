'use strict';

angular.module('symptom.controls', [])
    .directive("smallScaleSlider", function() {
        return {
            restrict: 'E',
            replace: true,
            scope: {
                givenResponse: '='
            },
            template: '<div kendo-slider ng-model="givenResponse[0]" class="balSlider" k-options="{ min : 0, max : 5, value : 0}"></div>'
        };
    })
    .directive("mediumScaleSlider", function() {
        return {
            restrict: 'E',
            replace: true,
            scope: {
                givenResponse: '='
            },
            template: '<div ng-model="givenResponse[0]" kendo-slider k-options="{ min : 0, max : 10, value : 0}"></div>'
        };
    })
    .directive("durationList", function() {
        return {
            restrict: 'E',
            replace: true,
            scope: {
                givenResponse: '='
            },
            template: '<select ng-model="givenResponse[0]" kendo-drop-down-list> ' +
                '<option value="0">Not Bothering Me</option>' +
                '<option value="1">1 - 3 Days</option>' +
                '<option value="2">3 - 7 Days</option>' +
                '<option value="3">2 - 4 Weeks</option>' +
                '<option value="4">1 - 2 Months</option>' +
                '<option value="5">3 - 6 Months</option>' +
                '<option value="6">6 - 12 Months</option>' +
                '<option value="7">1 - 3 Years</option>' +
                '<option value="8">3 Years or Greater</option>' +
                '</select>'
        };
    })
    .directive("booleanResponse", function() {
        return {
            restrict: 'E',
            replace: true,
            scope: {
                givenResponse: '='
            },
            template: '<div class="form-inline"><label class="radio">' +
                '<input type="radio" id="option2" value="1" ng-model="givenResponse[0]"> Yes ' +
                '</label>' +
                '<label style="padding-left:10px" class="radio">' +
                '<input type="radio" id="option1" value="0" ng-model="givenResponse[0]"> No ' +
                '</label></div>'
        };
    })
    .directive("whileList", function() {
        return {
            restrict: 'E',
            replace: true,
            scope: {
                givenResponse: '=',
            },
            link: function(scope, element, attrs) {
                scope.whileItems = [
                    { name: 'Sleeping', value: '1' },
                    { name: 'Prolonged Sitting', value: '2' },
                    { name: 'Sitting to Standing', value: '3' },
                    { name: 'Getting Dressed', value: '4' },
                    { name: 'Bending Over', value: '5' },
                    { name: 'Squatting / Kneeling', value: '6' },
                    { name: 'Lifting / Carrying', value: '7' },
                    { name: 'Reach / Throw / Swing', value: '8' },
                    { name: 'Walking', value: '9' },
                    { name: 'Running', value: '10' },
                    { name: 'Up / Down Stairs', value: '11' }
                ];

                scope.toggleSelection = function toggleSelection(whileItem) {
                    var idx = scope.givenResponse.indexOf(whileItem.value);

                    // is currently selected
                    if (idx > -1) {
                        scope.givenResponse.splice(idx, 1);
                    }
// is newly selected
                    else {
                        scope.givenResponse.push(whileItem.value);
                    }
                };
            },
            templateUrl: '/app/shared/symptoms/tmpl.while.symptom.htm'
        };
    }).directive("feelsLikeList", function() {
        return {
            restrict: 'E',
            replace: true,
            scope: {
                givenResponse: '=',
            },
            link: function(scope, element, attrs) {
                scope.feelItems = [
                    { name: 'Sharp / Stabbing ', value: '1' },
                    { name: 'Dull / Aching', value: '2' },
                    { name: 'Throbbing Pain', value: '3' },
                    { name: 'Stiff / Tight', value: '4' },
                    { name: 'Tingling / Numbness / Burning', value: '5' },
                    { name: 'Weak / Tired', value: '6' },
                    { name: 'Clicks / Snaps', value: '7' },
                    { name: 'Grinding / Bone-On-Bone', value: '8' },
                    { name: 'Buckles / Gives Out', value: '9' }
                ];

                scope.toggleSelection = function toggleSelection(item) {
                    var idx = scope.givenResponse.indexOf(item.value);

                    // is currently selected
                    if (idx > -1) {
                        scope.givenResponse.splice(idx, 1);
                    }
// is newly selected
                    else {
                        scope.givenResponse.push(item.value);
                    }
                };
            },
            templateUrl: '/app/shared/symptoms/tmpl.feels.symptom.htm'
        };
    });


