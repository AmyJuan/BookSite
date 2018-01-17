﻿///<reference path="../typing.d.ts"/>

import * as angular from 'angular';
//import controller from './index'
//import * as $ from 'jquery';

 //interface FunctionExport { default: any; }
 

//controller(app);


let app = angular.module('login', []);


require<FunctionExport>('./index').default(app);
//require("./index");

//let tmp = <any>( require<FunctionExport>('./index'));
//co
//tmp.controller(app);
angular.bootstrap(document, ["login"]);