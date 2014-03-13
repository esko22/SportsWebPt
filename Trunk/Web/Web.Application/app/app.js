'use strict';


var swptApp = angular.module('swptApp', ['ngResource', 'ui.router', 'ngAnimate', 'jquery.plugin.module', 'shared.ui', 'examine', 'research', 'ui.bootstrap', 'ngSanitize', 'kendo.directives'])
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
            .state('public.about',
            {
                url: "/",
                views: {
                    "core-app-view": { templateUrl: '/app/splash/prtl.about.htm' }
                }
            })
            .state('public.contact',
            {
                url: "/",
                views: {
                    "core-app-view": { templateUrl: '/app/splash/prtl.contact.htm' }
                }
            })
            .state('public.examine',
            {
                abstract: true,
                views: {
                    "core-app-view": {
                        templateUrl: '/app/examine/prtl.examine.htm'
                    }
                }
            })
            .state('public.examine.skeleton',
            {
                url: "/examine",
                views: {
                    "examine-app-view": {
                        templateUrl: '/app/examine/skeleton/prtl.examine.skeleton.htm'
                    }
                }
            })
            .state('public.examine.symptoms',
            {
                url: "/examine/symptoms",
                views: {
                    "examine-app-view": {
                        templateUrl: '/app/examine/symptoms/prtl.examine.symptoms.htm'
                    }
                }
            })
            .state('public.examine.report',
            {
                url: "/examine/report",
                views: {
                    "examine-app-view": {
                        templateUrl: '/app/examine/report/prtl.examine.report.htm'
                    }
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
                        controller: 'ExerciseListingController'
                }
            }
            })
          .state('public.research.exerciseDetail',
            {
                url: "/research/exercises/:exerciseId",
                views: {
                    "research-app-view": {
                        templateUrl: '/app/research/exercises/detail/prtl.exercise.view.htm',
                        controller: 'ExerciseViewController',
                        resolve: {
                            exercise: function ($q, exerciseDetailService, $stateParams) {
                                var deferred = $q.defer();
                                exerciseDetailService.getExercise($stateParams.exerciseId)
                                    .then(function(exercise) {
                                        deferred.resolve(exercise);
                                    });
                                return deferred.promise;
                            }
                        }
                    }
                }
            })
            .state('public.research.planDetail',
            {
                url: "/research/plans/:planId",
                views: {
                    "research-app-view": {
                        templateUrl: '/app/research/plans/detail/prtl.plan.view.htm',
                        controller: 'PlanViewController',
                        resolve: {
                            plan: function ($q, planDetailService, $stateParams) {
                                var deferred = $q.defer();
                                planDetailService.getPlan($stateParams.planId)
                                    .then(function (plan) {
                                        deferred.resolve(plan);
                                    });
                                return deferred.promise;
                            }
                        }
                    }
                }
            })
            .state('public.research.injuryDetail',
            {
                url: "/research/injuries/:injuryId",
                views: {
                    "research-app-view": {
                        templateUrl: '/app/research/injuries/detail/prtl.injury.view.htm',
                        controller: 'InjuryViewController',
                        resolve: {
                            injury: function ($q, injuryDetailService, $stateParams) {
                                var deferred = $q.defer();
                                injuryDetailService.getInjury($stateParams.injuryId)
                                    .then(function (injury) {
                                        deferred.resolve(injury);
                                    });
                                return deferred.promise;
                            }
                        }
                    }
                }
            });
        $urlRouterProvider.otherwise('/');
    });


var jQueryPluginModule = angular.module('jquery.plugin.module', []);


swptApp.factory('$exceptionHandler', function (notifierService) {
    return function (exception) {
        notifierService.error(exception.message);
        console.log("exception handled: " + exception.message);
    };
});

// Array Remove - By John Resig (MIT Licensed)
Array.prototype.remove = function (from, to) {
    var rest = this.slice((to || from) + 1 || this.length);
    this.length = from < 0 ? this.length + from : from;
    return this.push.apply(this, rest);
};

