'use strict';

var filterModule = angular.module('common.filters', []);

// For use with the built-in limitTo filter to accomplish paging.
// Expects the start position as a 1-based number.
// TODO: this filter fires multiple times when applying to an ng-repeat, need to figure out a workaround...
// http://www.bennadel.com/blog/2489-How-Often-Do-Filters-Execute-In-AngularJS.htm
filterModule.filter('startFrom', function () {
    return function (input, startPosition) {
        startPosition = startPosition - 1;  //convert to 0-based index
        if (!input) {
            return 0;
        }

        return input.slice(startPosition);
    };
});


filterModule.filter('moment', function () {
    return function (input, format) {
        return moment(parseInt(input)).utc().format(format);
    };
});

filterModule.filter('localTime', function () {
    return function (input) {
        return moment(input).local().format("MMMM DD, YYYY @ hh:mm A");
    };
});

filterModule.filter('unsafe', ['$sce', function ($sce) {
    return function (val) {
        return $sce.trustAsHtml(val);
    };
}]);