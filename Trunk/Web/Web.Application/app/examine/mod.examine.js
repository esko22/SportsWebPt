'use strict';

angular.module('examine', [])
    .controller('ExamineController', function ($scope) {
        $scope.selectedAreas = [];
    })
    .controller('ExamineSkeletonController', function ($scope, examineSkeletonService, configService) {
        $scope.maxSelectableAreas = configService.maxSelectableAreas;
        $scope.symptomaticRegions = configService.symptomaticRegions;

        $scope.onSelectedRegion = examineSkeletonService.onSelectedRegion;
        $scope.areaMouseover = examineSkeletonService.areaMouseover;
        $scope.areaMouseout = examineSkeletonService.areaMouseout;
        $scope.formatBodyParts = examineSkeletonService.formatBodyParts;

    })
    .factory('examineSkeletonService', function ($resource, $q, configService, notifierService) {

        var onSelectedRegion =  function (region, selectedAreas) {
            if (selectedAreas.indexOf(region) == -1) {
                if (selectedAreas.length < configService.maxSelectableAreas) {
                    $('#' + region.cssClassName).addClass('skeleton-selected');
                    selectedAreas.push(region);
                } else {
                    notifierService.warn('Only ' + configService.maxSelectableAreas + ' selectable areas allowed.');
                }
            } else {
                $('#' + region.cssClassName).removeClass('skeleton-selected');
                selectedAreas.remove(selectedAreas.indexOf(region));
            }
        };

        var areaMouseover = function (item, selectedAreas) {
            if (selectedAreas.indexOf(item) > -1) {
                $('#' + item.cssClassName).removeClass('skeleton-selected');
            }

            $('#' + item.cssClassName).addClass('skeleton-hover');
        };

        var areaMouseout = function (item, selectedAreas) {
            $('#' + item.cssClassName).removeClass('skeleton-hover');

            if (selectedAreas.indexOf(item) > -1) {
                $('#' + item.cssClassName).addClass('skeleton-selected');
            }
        };

        var formatBodyParts = function(region) {

            var html = "";

            angular.forEach(region.bodyParts, function(bodyPart) {
                if (!bodyPart.isSecondary)
                    html = $.validator.format('{0}<div>{1}</div>', html, bodyPart.commonName);
            });

            return html;
        };



        return {
            onSelectedRegion: onSelectedRegion,
            areaMouseover: areaMouseover,
            areaMouseout: areaMouseout,
            formatBodyParts: formatBodyParts
        };
    }
);
