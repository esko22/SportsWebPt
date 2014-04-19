angular.module('examine.skeleton', [])
    .controller('ExamineSkeletonController', ['$scope', 'examineSkeletonService', 'configService', function ($scope, examineSkeletonService, configService) {

        $scope.isLoading = true;
        $scope.currentStep.order = 1;

        configService.symptomaticRegions.$promise.then(function(regions) {
            $scope.symptomaticRegions = regions;
            $scope.isLoading = false;
        });


        $scope.maxSelectableAreas = configService.maxSelectableAreas;
         
        $scope.onSelectedRegion = examineSkeletonService.onSelectedRegion;
        $scope.areaMouseover = examineSkeletonService.areaMouseover;
        $scope.areaMouseout = examineSkeletonService.areaMouseout;
        $scope.formatBodyParts = examineSkeletonService.formatBodyParts;
        $scope.isSelected = examineSkeletonService.isSelected;

    }])
    .factory('examineSkeletonService', ['configService', 'notifierService', function (configService, notifierService) {

        var onSelectedRegion = function (region, selectedAreas) {
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

        var formatBodyParts = function (region) {

            var html = "";

            angular.forEach(region.bodyParts, function (bodyPart) {
                if (!bodyPart.isSecondary)
                    html = $.validator.format('{0}<div>{1}</div>', html, bodyPart.commonName);
            });

            return html;
        };

        var isSelected = function (region, selectedAreas) {
            return selectedAreas.indexOf(region) >= 0;
        };

        return {
            onSelectedRegion: onSelectedRegion,
            areaMouseover: areaMouseover,
            areaMouseout: areaMouseout,
            formatBodyParts: formatBodyParts,
            isSelected: isSelected
        };
    }]
);
