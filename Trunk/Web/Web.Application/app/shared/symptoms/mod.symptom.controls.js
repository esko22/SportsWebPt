'use strict';

angular.module('symptom.controls', [])
    .directive("smallScaleSlider", function () {
        return {
            restrict: 'E',
            replace: true,
            scope: {
                givenResponse: '='
            },
            template: '<div kendo-slider ng-model="givenResponse[0]" class="balSlider" k-options="{ min : 1, max : 5, value : 1}"></div>'
        };
    })
    .directive("mediumScaleSlider", function () {
        return {
            restrict: 'E',
            replace: true,
            scope: {
                givenResponse: '='
            },
            template: '<div id="slider" ng-model="givenResponse[0]" kendo-slider k-options="{ min : 1, max : 10, value : 1}"></div>'
        };
    })
    .directive("durationList", function () {
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
    .directive("booleanResponse", function () {
        return {
            restrict: 'E',
            replace: true,
            scope: {
                givenResponse: '='
            },
            template: '<div><label class="radio">' +
                      '<input type="radio" id="option1" value="0" ng-model="givenResponse[0]" >No ' +
                      '</label>'+ 
                      '<label class="radio">' +
                      '<input type="radio" id="option2" value="1" ng-model="givenResponse[0]">Yes ' +
                      '</label></div>'
        };
    })
    .directive("whileList", function () {
        return {
            restrict: 'E',
            replace: true,
            scope: {
                givenResponse: '=',
            },
            link: function(scope, element, attrs) {
                scope.whileItems = [
                    { name: 'Not Painful', value: 0 },
                    { name: 'Sleeping', value: 1 },
                    { name: 'Prolonged Sitting', value: 2 },
                    { name: 'Sitting to Standing', value: 3 },
                    { name: 'Getting Dressed', value: 4 },
                    { name: 'Bending Over', value: 5 },
                    { name: 'Squatting / Kneeling', value: 6 },
                    { name: 'Lifting / Carrying', value: 7 },
                    { name: 'Reach / Throw / Swing', value: 8 },
                    { name: 'Walking', value: 9 },
                    { name: 'Running', value: 10 },
                    { name: 'Up / Down Stairs', value: 11 }
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
            template:  '<div><label ng-repeat="whileItem in whileItems">' +
                       '<input type="checkbox" name="selectedWhileItems[]" value="{{whileItem.value}}"' +
                       'ng-checked="givenResponse.indexOf(whileItem.value) > -1" ng-click="toggleSelection(whileItem)"' +
                       '> {{whileItem.name}}</label><div>'
        };
    }).directive("feelsLikeList", function () {
        return {
            restrict: 'E',
            replace: true,
            scope: {
                givenResponse: '='
            },
            template: '<select multiple ng-model="givenResponse" size="12" data-placeholder="Select feels like..."> ' +
                        '<option value="0">Has No Feeling</option>' +
                        '<option value="1">Sharp / Stabbing </option>' +
                        '<option value="2">Dull / Aching </option>' +
                        '<option value="3">Throbbing Pain</option>' +
                        '<option value="4">Stiff / Tight </option>' +
                        '<option value="5">Tingling / Numbness / Burning</option>' +
                        '<option value="6">Weak / Tired </option>' +
                        '<option value="7">Clicks / Snaps</option>' +
                        '<option value="8">Grinding / Bone-On-Bone</option>' +
                        '<option value="9">Buckles / Gives Out</option>' +
                    '</select>'
        };
    });



//template: '<select multiple ng-model="givenResponse" size="11" data-placeholder="Select while...">' +
//            '<option value="0">Not Painful</option>' +
//            '<option value="1">Sleeping</option>' +
//            '<option value="2">Prolonged Sitting</option>' +
//            '<option value="3">Sitting to Standing</option>' +
//            '<option value="4">Getting Dressed</option>' +
//            '<option value="5">Bending Over</option>' +
//            '<option value="6">Squatting / Kneeling</option>' +
//            '<option value="7">Lifting / Carrying</option>' +
//            '<option value="8">Reach / Throw / Swing</option>' +
//            '<option value="9">Walking</option>' +
//            '<option value="10">Running</option>' +
//            '<option value="11">Up / Down Stairs</option>' +
//        '</select>'

