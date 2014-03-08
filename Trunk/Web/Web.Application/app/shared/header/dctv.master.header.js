'use strict';

swptApp.directive('masterHeader', function () {
        return {
            restrict: 'E',
            replace: 'true',
            templateUrl: '/app/shared/header/tmpl.master.header.htm' 
        };
    })
;