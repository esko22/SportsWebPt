'use strict';


var swptApp = angular.module('swptApp', ['ngResource', 'ui.router', 'ngAnimate', 'jquery.plugin.module', 'shared.ui', 'research', 'ui.bootstrap', 'ngSanitize'])
    .config(function ($urlRouterProvider, $stateProvider) {
        $stateProvider
            .state('user',
            {
                abstract: true,
                views: {
                    "core-view": { templateUrl: '/app/prtl.core.htm' }
                },
                data: {
                    access: 'access.user'
                }
            })
            .state('user.dashboard',
            {
                url: "/dashboard",
                views: {
                    "core-app-view": { templateUrl: '/app/dashboard/prtl.dashboard.htm' }
                }
            })
            .state('admin',
            {
                url: '/admin',
                abstract: true,
                views: {
                    "core-view": { templateUrl: '/app/prtl.app.htm' }
                },
                data: {
                    access: 'access.user'
                }
            })
            .state('admin.dashboard',
            {
                url: "/admin/dashboard",
                views: {
                    "admin-view": { templateUrl: '/app/dashboard/prtl.dashboard.htm' }
                }
            })
            .state('public',
            {
                abstract: true,
                views: {
                    "core-view": { templateUrl: '/app/prtl.core.htm' }
                },
                data: {
                    access: 'access.anon'
                }
            })
            .state('public.splash',
            {
                url: "/",
                views: {
                    "core-app-view": { templateUrl: '/app/splash/prtl.splash.htm' }
                }
            })
            .state('public.examine',
            {
                url: "/examine",
                views: {
                    "core-app-view": { templateUrl: '/app/examine/prtl.examine.htm' }
                }
            })
            .state('public.research',
            {
                abstract: true,
                views: {
                    "core-app-view": { templateUrl: '/app/research/prtl.research.htm' },
                }
            })
            .state('public.research.nav',
            {
                url: "/research",
                views: {
                    "research-app-view": { templateUrl: '/app/research/prtl.research.nav.htm' }
                }
            })
            .state('public.research.plans',
            {
                url: "/research/plans",
                views: {
                    "research-app-view": {
                        templateUrl: '/app/research/plans/prtl.research.plans.htm',
                        resolve: {
                            plans: function ($q, $resource, configService) {
                                var deferred = $q.defer();
                                var briefPlanProxy = $resource(configService.apiUris.briefPlans);
                                briefPlanProxy.query({}, function (plans) {
                                    deferred.resolve(plans);
                                });
                                return deferred.promise;
                            }
                        },
                        controller: 'PlanController'
                    }
                }
            })
            .state('public.research.injuries',
            {
                url: "/research/injuries",
                views: {
                    "research-app-view": {
                        templateUrl: '/app/research/injuries/prtl.research.injuries.htm',
                        resolve: {
                            injuries: function ($q, $resource, configService) {
                                var deferred = $q.defer();
                                var briefInjuryProxy = $resource(configService.apiUris.briefInjuries);
                                briefInjuryProxy.query({}, function (injuries) {
                                    deferred.resolve(injuries);
                                });
                                return deferred.promise;
                            }
                        },
                        controller: 'InjuryController'
                    }
                }
            })
            .state('public.research.locations',
            {
                url: "/research/locations",
                views: {
                    "research-app-view": { templateUrl: '/app/research/prtl.research.locations.htm' }
                }
            })
            .state('public.research.exercises',
            {
                url: "/research/exercises",
                views: {
                    "research-app-view": {
                        templateUrl: '/app/research/exercises/prtl.research.exercises.htm',
                        resolve: {
                            exercises: function ($q, $resource, configService) {
                                var deferred = $q.defer();
                                var briefExerciseProxy = $resource(configService.apiUris.briefExercises);
                                briefExerciseProxy.query({}, function(exercises) {
                                        deferred.resolve(exercises);
                                    });
                                return deferred.promise;
                            }
                        },
                    controller: 'ExerciseController'
                }
            }
        });
        $urlRouterProvider.otherwise('/');
    });


var jQueryPluginModule = angular.module('jquery.plugin.module', []);


swptApp.factory('$exceptionHandler', function (notifierService) {
    return function (exception) {
        notifierService.error("Internal Error Occurred");
        console.log("exception handled: " + exception.message);
    };
});





//var sportsWebPtApp = angular.module('sportsWebPtApp', ['ngResource', 'ngSanitize', 'ui-router','ngAnimate'])
//    .config(function ($routeProvider, $locationProvider) {
//    $routeProvider.when('/research/exercises/:exerciseId',
//        {
//            templateUrl: 'app/research/exerciseDetail/prtl-exerciseDetail.htm',
//            controller: 'ExerciseDetailController',
//            resolve: {
//                exercise: function($q, $route, exerciseDetailService) {
//                    var deferred = $q.defer();
//                    exerciseDetailService.getExercise($route.current.pathParams.exerciseId)
//                        .then(function(exercise) {
//                            deferred.resolve(exercise);
//                        });
//                    return deferred.promise;
//                }
//            }
//        }),
//        $routeProvider.when('/',
//        {
//            templateUrl: 'app/splash/prtl-splash.htm'
//        });
//        $routeProvider.when('/research',
//        {
//            templateUrl: 'app/research/prtl-research.htm'
//        });

//    $routeProvider.otherwise({redirectTo: ''});

//});
