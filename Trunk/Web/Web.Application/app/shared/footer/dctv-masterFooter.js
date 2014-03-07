'use strict';

swptApp.directive('masterFooter', function () {
        return {
            restrict: 'E',
            replace: 'true',
            templateUrl: '/app/shared/footer/tmpl-masterFooter.htm' 
        };
    })
;