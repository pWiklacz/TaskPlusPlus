"use strict";(self.webpackChunkclient=self.webpackChunkclient||[]).push([[953],{2953:(pe,g,d)=>{d.r(g),d.d(g,{AccountModule:()=>le});var p=d(6814),n=d(95),c=d(5219),e=d(9468),m=d(3624),l=d(2401),x=d(6451),u=d(4874);function b(t,s){if(1&t&&(e.TgZ(0,"div",31)(1,"span"),e._uU(2),e.qZA()()),2&t){const o=e.oxw();e.xp6(2),e.Oqu(o.errorMessage)}}function w(t,s){1&t&&(e.TgZ(0,"div"),e._UZ(1,"i",34),e._uU(2," Email is required"),e.qZA())}function Z(t,s){1&t&&(e.TgZ(0,"div"),e._UZ(1,"i",34),e._uU(2," Email must be a valid email address"),e.qZA())}function y(t,s){if(1&t&&(e.TgZ(0,"div",32),e.YNc(1,w,3,0,"div",33),e.YNc(2,Z,3,0,"div",33),e.qZA()),2&t){const o=e.oxw();e.xp6(1),e.Q6J("ngIf",null==o.form.email.errors?null:o.form.email.errors.required),e.xp6(1),e.Q6J("ngIf",null==o.form.email.errors?null:o.form.email.errors.email)}}function P(t,s){1&t&&(e.TgZ(0,"div"),e._UZ(1,"i",34),e._uU(2," Password is required"),e.qZA())}function C(t,s){if(1&t&&(e.TgZ(0,"div",32),e.YNc(1,P,3,0,"div",33),e.qZA()),2&t){const o=e.oxw();e.xp6(1),e.Q6J("ngIf",null==o.form.password.errors?null:o.form.password.errors.required)}}let T=(()=>{class t{constructor(o,i,r,a){this.accountService=o,this.router=i,this.activatedRoute=r,this.messageService=a,this.type="password",this.isText=!1,this.eyeIcon="fa-eye-slash",this.loginForm=new n.cw({email:new n.NI("",[n.kI.required,n.kI.email]),password:new n.NI("",n.kI.required)}),this.submitted=!1,this.externalLogin=()=>{this.accountService.signInWithFB()},this.returnUrl=this.activatedRoute.snapshot.queryParams.returnUrl||"/"}get form(){return this.loginForm.controls}onSubmit(){this.submitted=!0,!this.loginForm.invalid&&this.accountService.login(this.loginForm.value).subscribe({next:()=>this.router.navigateByUrl("dashboard/inbox"),error:o=>{console.log(o),this.errorMessage=o.message}})}hideShowPass(){this.isText=!this.isText,this.eyeIcon=this.isText?"fa-eye":"fa-eye-slash",this.type=this.isText?"text":"password"}static#e=this.\u0275fac=function(i){return new(i||t)(e.Y36(m.B),e.Y36(l.F0),e.Y36(l.gz),e.Y36(c.ez))};static#t=this.\u0275cmp=e.Xpm({type:t,selectors:[["app-login"]],features:[e._Bn([c.ez])],decls:41,vars:16,consts:[[1,"outer"],[1,"container"],[1,"row","d-flex","align-items-center","justify-content-center"],[1,"col-lg-5","col-md-7"],[1,"panel","border","bg-white"],[1,"panel-heading"],[1,"pt-3","font-weight-bold"],["class","text-danger ps-3","role","alert",4,"ngIf"],[1,"panel-body","p-3"],[3,"formGroup","ngSubmit"],[1,"form-group","pt-3"],[1,"input-field"],[1,"fa","fa-envelope","p-2","text-primary"],["formControlName","email","type","text","placeholder","Email",1,"form-control"],["class","errorMessage",4,"ngIf"],[1,"fa","fa-lock","p-2","text-primary"],["formControlName","password","placeholder","Password",1,"form-control",3,"type"],[3,"click"],[1,"form-inline","fr","mt-4"],["id","forget","routerLink","/account/forgotpassword",1,"font-weight-bold"],["type","submit",1,"btn","btn-primary","btn-block","mt-4","w-100"],[1,"mx-3"],[1,"text-center","py-3"],["type","button",1,"btn","btn-link","btn-floating","mx-1",3,"click"],[1,"fa","fa-facebook-f","fa-lg","text-primary"],["type","icon","size","medium",1,"btn","btn-link","btn-floating","mx-1"],["type","button",1,"btn","btn-link","btn-floating","mx-1"],[1,"fa","fa-twitter","fa-lg","text-primary"],[1,"fa","fa-github","fa-lg","text-primary"],[1,"text-center","text-muted"],["id","forget","routerLink","/account/register"],["role","alert",1,"text-danger","ps-3"],[1,"errorMessage"],[4,"ngIf"],["aria-hidden","true",1,"fa","fa-exclamation-circle"]],template:function(i,r){1&i&&(e.TgZ(0,"div",0),e._UZ(1,"p-toast"),e.TgZ(2,"div",1)(3,"div",2)(4,"div",3)(5,"div",4)(6,"div",5)(7,"h3",6),e._uU(8,"Login"),e.qZA()(),e.YNc(9,b,3,1,"div",7),e.TgZ(10,"div",8)(11,"form",9),e.NdJ("ngSubmit",function(){return r.onSubmit()}),e.TgZ(12,"div",10)(13,"div",11),e._UZ(14,"span",12)(15,"input",13),e.qZA(),e.YNc(16,y,3,2,"div",14),e.qZA(),e.TgZ(17,"div",10)(18,"div",11),e._UZ(19,"span",15)(20,"input",16),e.TgZ(21,"span",17),e.NdJ("click",function(){return r.hideShowPass()}),e.qZA()(),e.YNc(22,C,2,1,"div",14),e.qZA(),e.TgZ(23,"div",18)(24,"a",19),e._uU(25,"Forget Password?"),e.qZA()(),e.TgZ(26,"button",20),e._uU(27,"Login"),e.qZA(),e.TgZ(28,"div",21)(29,"div",22)(30,"button",23),e.NdJ("click",function(){return r.externalLogin()}),e._UZ(31,"i",24),e.qZA(),e._UZ(32,"asl-google-signin-button",25),e.TgZ(33,"button",26),e._UZ(34,"i",27),e.qZA(),e.TgZ(35,"button",26),e._UZ(36,"i",28),e.qZA()()(),e.TgZ(37,"div",29),e._uU(38,"Don't have account? "),e.TgZ(39,"a",30),e._uU(40,"Sign Up"),e.qZA()()()()()()()()()),2&i&&(e.xp6(9),e.Q6J("ngIf",r.errorMessage),e.xp6(2),e.Q6J("formGroup",r.loginForm),e.xp6(2),e.ekj("error",r.submitted&&r.form.email.invalid),e.xp6(2),e.ekj("is-invalid",r.submitted&&r.form.email.invalid),e.xp6(1),e.Q6J("ngIf",r.submitted&&r.form.email.errors),e.xp6(2),e.ekj("error",r.submitted&&r.form.password.invalid),e.xp6(2),e.ekj("is-invalid",r.submitted&&r.form.password.invalid),e.Q6J("type",r.type),e.xp6(1),e.Gre("fa ",r.eyeIcon," text-primary"),e.xp6(1),e.Q6J("ngIf",r.submitted&&r.form.email.errors))},dependencies:[p.O5,l.rH,n._Y,n.Fj,n.JJ,n.JL,n.sg,n.u,x.FN,u.TI],styles:[".outer[_ngcontent-%COMP%]{height:100vh;background:linear-gradient(to top,#c9c9ff 50%,#6f3dbc 90%) no-repeat}.panel-heading[_ngcontent-%COMP%]{text-align:center;margin-bottom:10px}#forget[_ngcontent-%COMP%]{min-width:100px;margin-left:auto;text-decoration:none;cursor:pointer}a[_ngcontent-%COMP%]:hover{text-decoration:none}.form-inline[_ngcontent-%COMP%]   label[_ngcontent-%COMP%]{padding-left:10px;margin:0;cursor:pointer}.btn[_ngcontent-%COMP%]{margin-top:20px;border-radius:15px}.errorMessage[_ngcontent-%COMP%]{color:red;padding-left:10px}.panel[_ngcontent-%COMP%]{min-height:380px;box-shadow:20px 20px 80px #dadada;border-radius:12px;margin-top:100px}.input-field[_ngcontent-%COMP%]{border-radius:5px;padding:5px;display:flex;align-items:center;cursor:pointer;border:1px solid #ddd;color:#4343ff}.input-field.error[_ngcontent-%COMP%]{border:1px solid red;color:red}input[type=text][_ngcontent-%COMP%], input[type=password][_ngcontent-%COMP%]{border:none;outline:none;box-shadow:none;width:100%}.fa-eye-slash.btn[_ngcontent-%COMP%]{border:none;outline:none;box-shadow:none}img[_ngcontent-%COMP%]{width:40px;height:40px;object-fit:cover;border-radius:50%;position:relative}"]})}return t})();var _=d(381),h=d(553);function q(t,s){if(1&t&&(e.TgZ(0,"li",37)(1,"b"),e._uU(2),e.qZA()()),2&t){const o=s.$implicit;e.xp6(2),e.Oqu(o)}}function M(t,s){if(1&t&&(e.TgZ(0,"ul",35),e.YNc(1,q,3,1,"li",36),e.qZA()),2&t){const o=e.oxw();e.xp6(1),e.Q6J("ngForOf",o.errors)}}function I(t,s){1&t&&(e.TgZ(0,"div"),e._UZ(1,"i",40),e._uU(2," First name is required"),e.qZA())}function U(t,s){if(1&t&&(e.TgZ(0,"div",38),e.YNc(1,I,3,0,"div",39),e.qZA()),2&t){const o=e.oxw();e.xp6(1),e.Q6J("ngIf",null==o.form.firstName.errors?null:o.form.firstName.errors.required)}}function N(t,s){1&t&&(e.TgZ(0,"div"),e._UZ(1,"i",40),e._uU(2," Last name is required"),e.qZA())}function O(t,s){if(1&t&&(e.TgZ(0,"div",38),e.YNc(1,N,3,0,"div",39),e.qZA()),2&t){const o=e.oxw();e.xp6(1),e.Q6J("ngIf",null==o.form.lastName.errors?null:o.form.lastName.errors.required)}}function A(t,s){1&t&&(e.TgZ(0,"div"),e._UZ(1,"i",40),e._uU(2," Email is required"),e.qZA())}function k(t,s){1&t&&(e.TgZ(0,"div"),e._UZ(1,"i",40),e._uU(2," Email must be a valid email address"),e.qZA())}function J(t,s){if(1&t&&(e.TgZ(0,"div",38),e.YNc(1,A,3,0,"div",39),e.YNc(2,k,3,0,"div",39),e.qZA()),2&t){const o=e.oxw();e.xp6(1),e.Q6J("ngIf",null==o.form.email.errors?null:o.form.email.errors.required),e.xp6(1),e.Q6J("ngIf",null==o.form.email.errors?null:o.form.email.errors.email)}}function Y(t,s){1&t&&(e.TgZ(0,"div"),e._UZ(1,"i",40),e._uU(2," Password is required"),e.qZA())}function S(t,s){1&t&&(e.TgZ(0,"div"),e._UZ(1,"i",40),e._uU(2," Password should have at least 6 characters long, contain at least one digit, one lowercase letter, one uppercase letter, and one special character"),e.qZA())}function F(t,s){if(1&t&&(e.TgZ(0,"div",38),e.YNc(1,Y,3,0,"div",39),e.YNc(2,S,3,0,"div",39),e.qZA()),2&t){const o=e.oxw();e.xp6(1),e.Q6J("ngIf",null==o.form.password.errors?null:o.form.password.errors.required),e.xp6(1),e.Q6J("ngIf",null==o.form.password.errors?null:o.form.password.errors.pattern)}}function Q(t,s){1&t&&(e.TgZ(0,"div"),e._UZ(1,"i",40),e._uU(2," Confirmation is required"),e.qZA())}function R(t,s){1&t&&(e.TgZ(0,"div"),e._UZ(1,"i",40),e._uU(2," Passwords must match"),e.qZA())}function j(t,s){if(1&t&&(e.TgZ(0,"div",38),e.YNc(1,Q,3,0,"div",39),e.YNc(2,R,3,0,"div",39),e.qZA()),2&t){const o=e.oxw();e.xp6(1),e.Q6J("ngIf",null==o.form.confirmPassword.errors?null:o.form.confirmPassword.errors.required),e.xp6(1),e.Q6J("ngIf",null==o.form.confirmPassword.errors?null:o.form.confirmPassword.errors.mustMatch)}}function E(t,s){1&t&&(e.TgZ(0,"div"),e._UZ(1,"i",19),e._uU(2," Email is required"),e.qZA())}function B(t,s){1&t&&(e.TgZ(0,"div"),e._UZ(1,"i",19),e._uU(2," Email must be a valid email address"),e.qZA())}function z(t,s){if(1&t&&(e.TgZ(0,"div",17),e.YNc(1,E,3,0,"div",18),e.YNc(2,B,3,0,"div",18),e.qZA()),2&t){const o=e.oxw();e.xp6(1),e.Q6J("ngIf",null==o.form.email.errors?null:o.form.email.errors.required),e.xp6(1),e.Q6J("ngIf",null==o.form.email.errors?null:o.form.email.errors.email)}}function $(t,s){if(1&t&&(e.TgZ(0,"li",21)(1,"b"),e._uU(2),e.qZA()()),2&t){const o=s.$implicit;e.xp6(2),e.Oqu(o)}}function D(t,s){if(1&t&&(e.TgZ(0,"ul",19),e.YNc(1,$,3,1,"li",20),e.qZA()),2&t){const o=e.oxw();e.xp6(1),e.Q6J("ngForOf",o.errors)}}function X(t,s){1&t&&(e.TgZ(0,"div"),e._UZ(1,"i",24),e._uU(2," Password is required"),e.qZA())}function H(t,s){1&t&&(e.TgZ(0,"div"),e._UZ(1,"i",24),e._uU(2," Password should have at least 6 characters long, contain at least one digit, one lowercase letter, one uppercase letter, and one special character"),e.qZA())}function V(t,s){if(1&t&&(e.TgZ(0,"div",22),e.YNc(1,X,3,0,"div",23),e.YNc(2,H,3,0,"div",23),e.qZA()),2&t){const o=e.oxw();e.xp6(1),e.Q6J("ngIf",null==o.form.password.errors?null:o.form.password.errors.required),e.xp6(1),e.Q6J("ngIf",null==o.form.password.errors?null:o.form.password.errors.pattern)}}function W(t,s){1&t&&(e.TgZ(0,"div"),e._UZ(1,"i",24),e._uU(2," Confirmation is required"),e.qZA())}function K(t,s){1&t&&(e.TgZ(0,"div"),e._UZ(1,"i",24),e._uU(2," Passwords must match"),e.qZA())}function ee(t,s){if(1&t&&(e.TgZ(0,"div",22),e.YNc(1,W,3,0,"div",23),e.YNc(2,K,3,0,"div",23),e.qZA()),2&t){const o=e.oxw();e.xp6(1),e.Q6J("ngIf",null==o.form.confirmPassword.errors?null:o.form.confirmPassword.errors.required),e.xp6(1),e.Q6J("ngIf",null==o.form.confirmPassword.errors?null:o.form.confirmPassword.errors.mustMatch)}}function re(t,s){if(1&t&&(e.TgZ(0,"div",9),e._uU(1),e.qZA()),2&t){const o=e.oxw();e.xp6(1),e.hij(" ",o.errorMessage," ")}}const oe=function(){return["/account/login"]};function ne(t,s){1&t&&(e.TgZ(0,"div",10),e._uU(1," Your email has been successfully confirmed. Please "),e.TgZ(2,"a",11),e._uU(3," click here to log in. "),e.qZA()()),2&t&&(e.xp6(2),e.Q6J("routerLink",e.DdM(1,oe)))}const ie=[{path:"login",component:T},{path:"register",component:(()=>{class t{constructor(o,i){this.accountService=o,this.router=i,this.clientUrl=h.N.clientUrl,this.errors=null,this.submitted=!1,this.type="password",this.isText=!1,this.eyeIcon="fa-eye-slash",this.complexPassword="(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*()_+{}\":;'?/>.<,])(?!.*\\s).*$",this.registerForm=new n.cw({firstName:new n.NI("",n.kI.required),lastName:new n.NI("",n.kI.required),email:new n.NI("",[n.kI.required,n.kI.email]),password:new n.NI("",[n.kI.required,n.kI.pattern(this.complexPassword)]),confirmPassword:new n.NI("",n.kI.required)},{validators:(0,_.Y)("password","confirmPassword")})}get form(){return this.registerForm.controls}onSubmit(){if(this.submitted=!0,this.registerForm.invalid)return;this.errors=[];const o=this.registerForm.value;this.accountService.register({firstName:o.firstName,lastName:o.lastName,email:o.email,password:o.password,clientURI:this.clientUrl+"account/emailconfirmation"}).subscribe({next:()=>this.router.navigateByUrl("account/login"),error:r=>{r.value?this.errors=r.value:this.errors?.push(r.message)}})}hideShowPass(){this.isText=!this.isText,this.eyeIcon=this.isText?"fa-eye":"fa-eye-slash",this.type=this.isText?"text":"password"}static#e=this.\u0275fac=function(i){return new(i||t)(e.Y36(m.B),e.Y36(l.F0))};static#t=this.\u0275cmp=e.Xpm({type:t,selectors:[["app-register"]],decls:54,vars:35,consts:[[1,"outer"],[1,"container"],[1,"row","d-flex","align-items-center","justify-content-center"],[1,"col-lg-5","col-md-7"],[1,"panel","border","bg-white"],[1,"panel-heading"],[1,"py-3","font-weight-bold"],["class","text-danger list-unstyled",4,"ngIf"],[1,"panel-body","px-3","pb-3"],[3,"formGroup","ngSubmit"],[1,"row"],[1,"form-group","pt-3","col-md-6","mb-2"],[1,"input-field"],[1,"fa","fa-user","fa-fw","me-1","text-primary"],["formControlName","firstName","type","text","placeholder","FirstName",1,"form-control"],["class","errorMessage",4,"ngIf"],["formControlName","lastName","type","text","placeholder","LastName",1,"form-control"],[1,"form-group","pt-3"],[1,"fa","fa-envelope","p-2","text-primary"],["formControlName","email","type","text","placeholder","Email",1,"form-control"],[1,"fa","fa-lock","p-2","text-primary"],["formControlName","password","placeholder","Password",1,"form-control",3,"type"],[3,"click"],[1,"fa","fa-key","fa-fw","me-1","text-primary"],["formControlName","confirmPassword","placeholder","Confirm password",1,"form-control",3,"type"],["type","submit",1,"btn","btn-primary","btn-block","mt-4","w-100"],[1,"mx-3"],[1,"text-center","py-3"],["type","button",1,"btn","btn-link","btn-floating","mx-1"],[1,"fa","fa-facebook-f","fa-lg","text-primary"],["type","icon","size","medium",1,"btn","btn-link","btn-floating","mx-1"],[1,"fa","fa-twitter","fa-lg","text-primary"],[1,"fa","fa-github","fa-lg","text-primary"],[1,"text-center","text-muted"],["id","forget","routerLink","/account/login"],[1,"text-danger","list-unstyled"],["class","ps-3",4,"ngFor","ngForOf"],[1,"ps-3"],[1,"errorMessage"],[4,"ngIf"],["aria-hidden","true",1,"fa","fa-exclamation-circle"]],template:function(i,r){1&i&&(e.TgZ(0,"div",0)(1,"div",1)(2,"div",2)(3,"div",3)(4,"div",4)(5,"div",5)(6,"h3",6),e._uU(7,"Sign Up"),e.qZA()(),e.YNc(8,M,2,1,"ul",7),e.TgZ(9,"div",8)(10,"form",9),e.NdJ("ngSubmit",function(){return r.onSubmit()}),e.TgZ(11,"div",10)(12,"div",11)(13,"div",12),e._UZ(14,"span",13)(15,"input",14),e.qZA(),e.YNc(16,U,2,1,"div",15),e.qZA(),e.TgZ(17,"div",11)(18,"div",12),e._UZ(19,"span",13)(20,"input",16),e.qZA(),e.YNc(21,O,2,1,"div",15),e.qZA()(),e.TgZ(22,"div",17)(23,"div",12),e._UZ(24,"span",18)(25,"input",19),e.qZA(),e.YNc(26,J,3,2,"div",15),e.qZA(),e.TgZ(27,"div",17)(28,"div",12),e._UZ(29,"span",20)(30,"input",21),e.TgZ(31,"span",22),e.NdJ("click",function(){return r.hideShowPass()}),e.qZA()(),e.YNc(32,F,3,2,"div",15),e.qZA(),e.TgZ(33,"div",17)(34,"div",12),e._UZ(35,"span",23)(36,"input",24),e.TgZ(37,"span",22),e.NdJ("click",function(){return r.hideShowPass()}),e.qZA()(),e.YNc(38,j,3,2,"div",15),e.qZA(),e.TgZ(39,"button",25),e._uU(40,"Sign Up"),e.qZA(),e.TgZ(41,"div",26)(42,"div",27)(43,"button",28),e._UZ(44,"i",29),e.qZA(),e._UZ(45,"asl-google-signin-button",30),e.TgZ(46,"button",28),e._UZ(47,"i",31),e.qZA(),e.TgZ(48,"button",28),e._UZ(49,"i",32),e.qZA()()(),e.TgZ(50,"div",33),e._uU(51,"Already have account? "),e.TgZ(52,"a",34),e._uU(53,"Login"),e.qZA()()()()()()()()()),2&i&&(e.xp6(8),e.Q6J("ngIf",r.errors),e.xp6(2),e.Q6J("formGroup",r.registerForm),e.xp6(3),e.ekj("error",r.submitted&&r.form.firstName.invalid),e.xp6(2),e.ekj("is-invalid",r.submitted&&r.form.firstName.invalid),e.xp6(1),e.Q6J("ngIf",r.submitted&&r.form.firstName.errors),e.xp6(2),e.ekj("error",r.submitted&&r.form.lastName.invalid),e.xp6(2),e.ekj("is-invalid",r.submitted&&r.form.lastName.invalid),e.xp6(1),e.Q6J("ngIf",r.submitted&&r.form.lastName.errors),e.xp6(2),e.ekj("error",r.submitted&&r.form.email.invalid),e.xp6(2),e.ekj("is-invalid",r.submitted&&r.form.email.invalid),e.xp6(1),e.Q6J("ngIf",r.submitted&&r.form.email.errors),e.xp6(2),e.ekj("error",r.submitted&&r.form.password.invalid),e.xp6(2),e.ekj("is-invalid",r.submitted&&r.form.password.invalid),e.Q6J("type",r.type),e.xp6(1),e.Gre("fa ",r.eyeIcon," text-primary"),e.xp6(1),e.Q6J("ngIf",r.submitted&&r.form.password.errors),e.xp6(2),e.ekj("error",r.submitted&&r.form.confirmPassword.invalid),e.xp6(2),e.ekj("is-invalid",r.submitted&&r.form.confirmPassword.invalid),e.Q6J("type",r.type),e.xp6(1),e.Gre("fa ",r.eyeIcon," text-primary"),e.xp6(1),e.Q6J("ngIf",r.submitted&&r.form.confirmPassword.errors))},dependencies:[p.sg,p.O5,l.rH,n._Y,n.Fj,n.JJ,n.JL,n.sg,n.u,u.TI],styles:[".outer[_ngcontent-%COMP%]{height:100vh;background:linear-gradient(to top,#c9c9ff 50%,#6f3dbc 90%) no-repeat}.panel-heading[_ngcontent-%COMP%]{text-align:center;margin-bottom:10px}#forget[_ngcontent-%COMP%]{min-width:100px;margin-left:auto;text-decoration:none;cursor:pointer}a[_ngcontent-%COMP%]:hover{text-decoration:none}.form-inline[_ngcontent-%COMP%]   label[_ngcontent-%COMP%]{padding-left:10px;margin:0;cursor:pointer}.btn[_ngcontent-%COMP%]{margin-top:20px;border-radius:15px}.panel[_ngcontent-%COMP%]{min-height:380px;box-shadow:20px 20px 80px #dadada;border-radius:12px;margin-top:100px}.input-field[_ngcontent-%COMP%]{border-radius:5px;padding:5px;display:flex;align-items:center;cursor:pointer;border:1px solid #ddd;color:#4343ff}.errorMessage[_ngcontent-%COMP%]{color:red;padding-left:10px}.input-field.error[_ngcontent-%COMP%]{border:1px solid red;color:red}input[type=text][_ngcontent-%COMP%], input[type=password][_ngcontent-%COMP%]{border:none;outline:none;box-shadow:none;width:100%}.fa-eye-slash.btn[_ngcontent-%COMP%]{border:none;outline:none;box-shadow:none}img[_ngcontent-%COMP%]{width:40px;height:40px;object-fit:cover;border-radius:50%;position:relative}"]})}return t})()},{path:"forgotpassword",component:(()=>{class t{constructor(o,i){this.accountService=o,this.messageService=i,this.clientUrl=h.N.clientUrl,this.submitted=!1,this.forgotPasswordForm=new n.cw({email:new n.NI("",[n.kI.required,n.kI.email])})}get form(){return this.forgotPasswordForm.controls}onSubmit(o){this.submitted=!0,this.forgotPasswordForm.invalid||this.accountService.forgotPassword({email:o,clientUri:this.clientUrl+"account/resetpassword"}).subscribe({next:r=>{this.messageService.add({severity:"success",summary:"Success",detail:r.message,life:1e4})},error:r=>{this.errorMessage=r.message}})}static#e=this.\u0275fac=function(i){return new(i||t)(e.Y36(m.B),e.Y36(c.ez))};static#t=this.\u0275cmp=e.Xpm({type:t,selectors:[["app-forgot-password"]],decls:20,vars:6,consts:[[1,"outer"],[1,"container"],[1,"row","d-flex","align-items-center","justify-content-center"],[1,"col-lg-5","col-md-7"],[1,"panel","border","bg-white"],[1,"panel-heading"],[1,"pt-3","font-weight-bold"],[1,"panel-body","p-3"],[3,"formGroup","ngSubmit"],[1,"form-group","pt-3"],[1,"input-field"],[1,"fa","fa-envelope","p-2","text-primary"],["formControlName","email","type","text","placeholder","Email",1,"form-control"],["class","errorMessage",4,"ngIf"],["type","submit",1,"btn","btn-primary","btn-block","mt-4","w-100"],[1,"mt-3",2,"text-align","center"],["routerLink","/account/login",1,"center"],[1,"errorMessage"],[4,"ngIf"],["aria-hidden","true",1,"fa","fa-exclamation-circle"]],template:function(i,r){1&i&&(e.TgZ(0,"div",0)(1,"div",1)(2,"div",2)(3,"div",3)(4,"div",4)(5,"div",5)(6,"h1",6),e._uU(7,"Password recovery"),e.qZA()(),e.TgZ(8,"div",7)(9,"form",8),e.NdJ("ngSubmit",function(){return r.onSubmit(r.form.email.value)}),e.TgZ(10,"div",9)(11,"div",10),e._UZ(12,"span",11)(13,"input",12),e.qZA(),e.YNc(14,z,3,2,"div",13),e.qZA(),e.TgZ(15,"button",14),e._uU(16,"Continue"),e.qZA(),e.TgZ(17,"p",15)(18,"a",16),e._uU(19,"Back to Sign in"),e.qZA()()()()()()()()()),2&i&&(e.xp6(9),e.Q6J("formGroup",r.forgotPasswordForm),e.xp6(2),e.ekj("error",r.submitted&&r.form.email.invalid),e.xp6(2),e.ekj("is-invalid",r.submitted&&r.form.email.invalid),e.xp6(1),e.Q6J("ngIf",r.submitted&&r.form.email.errors))},dependencies:[p.O5,l.rH,n._Y,n.Fj,n.JJ,n.JL,n.sg,n.u],styles:[".outer[_ngcontent-%COMP%]{height:100vh;background:linear-gradient(to top,#c9c9ff 50%,#6f3dbc 90%) no-repeat}.panel-heading[_ngcontent-%COMP%]{text-align:center;margin-bottom:10px}#forget[_ngcontent-%COMP%]{min-width:100px;margin-left:auto;text-decoration:none;cursor:pointer}a[_ngcontent-%COMP%]:hover{text-decoration:none}.form-inline[_ngcontent-%COMP%]   label[_ngcontent-%COMP%]{padding-left:10px;margin:0;cursor:pointer}.btn[_ngcontent-%COMP%]{margin-top:20px;border-radius:15px}.errorMessage[_ngcontent-%COMP%]{color:red;padding-left:10px}.panel[_ngcontent-%COMP%]{min-height:200px;box-shadow:20px 20px 80px #dadada;border-radius:12px;margin-top:100px}.input-field[_ngcontent-%COMP%]{border-radius:5px;padding:5px;display:flex;align-items:center;cursor:pointer;border:1px solid #ddd;color:#4343ff}.input-field.error[_ngcontent-%COMP%]{border:1px solid red;color:red}input[type=text][_ngcontent-%COMP%], input[type=password][_ngcontent-%COMP%]{border:none;outline:none;box-shadow:none;width:100%}.fa-eye-slash.btn[_ngcontent-%COMP%]{border:none;outline:none;box-shadow:none}img[_ngcontent-%COMP%]{width:40px;height:40px;object-fit:cover;border-radius:50%;position:relative}"]})}return t})()},{path:"resetpassword",component:(()=>{class t{constructor(o,i,r,a,f){this.fb=o,this.accountService=i,this.router=r,this.route=a,this.messageService=f,this.errors=null,this.submitted=!1,this.type="password",this.isText=!1,this.eyeIcon="fa-eye-slash",this.complexPassword="(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*()_+{}\":;'?/>.<,])(?!.*\\s).*$",this.resetPasswordForm=new n.cw({password:new n.NI("",[n.kI.required,n.kI.pattern(this.complexPassword)]),confirmPassword:new n.NI("",n.kI.required)},{validators:(0,_.Y)("password","confirmPassword")})}ngOnInit(){this.token=this.route.snapshot.queryParams.token,this.email=this.route.snapshot.queryParams.email}get form(){return this.resetPasswordForm.controls}onSubmit(){if(this.submitted=!0,this.resetPasswordForm.invalid)return;const o=this.resetPasswordForm.value,i={password:o.password,confirmPassword:o.confirmPassword,token:this.token,email:this.email};this.errors=[],this.accountService.resetPassword(i).subscribe({next:r=>{this.router.navigateByUrl("/account/login").then(()=>{this.messageService.add({severity:"success",summary:r.message,life:1e4})})},error:r=>{r.value?this.errors=r.value:this.errors?.push(r.message)}})}hideShowPass(){this.isText=!this.isText,this.eyeIcon=this.isText?"fa-eye":"fa-eye-slash",this.type=this.isText?"text":"password"}static#e=this.\u0275fac=function(i){return new(i||t)(e.Y36(n.qu),e.Y36(m.B),e.Y36(l.F0),e.Y36(l.gz),e.Y36(c.ez))};static#t=this.\u0275cmp=e.Xpm({type:t,selectors:[["app-reset-password"]],decls:25,vars:20,consts:[[1,"outer"],[1,"container"],[1,"row","d-flex","align-items-center","justify-content-center"],[1,"col-lg-5","col-md-7"],[1,"panel","border","bg-white"],[1,"panel-heading"],[1,"py-3","font-weight-bold"],["class","text-danger list-unstyled",4,"ngIf"],[1,"panel-body","px-3","pb-3"],[3,"formGroup","ngSubmit"],[1,"form-group","pt-3"],[1,"input-field"],[1,"fa","fa-lock","p-2","text-primary"],["formControlName","password","placeholder","Password",1,"form-control",3,"type"],[3,"click"],["class","errorMessage",4,"ngIf"],[1,"fa","fa-key","fa-fw","me-1","text-primary"],["formControlName","confirmPassword","placeholder","Confirm password",1,"form-control",3,"type"],["type","submit",1,"btn","btn-primary","btn-block","mt-4","w-100"],[1,"text-danger","list-unstyled"],["class","ps-3",4,"ngFor","ngForOf"],[1,"ps-3"],[1,"errorMessage"],[4,"ngIf"],["aria-hidden","true",1,"fa","fa-exclamation-circle"]],template:function(i,r){1&i&&(e.TgZ(0,"div",0)(1,"div",1)(2,"div",2)(3,"div",3)(4,"div",4)(5,"div",5)(6,"h1",6),e._uU(7,"Reset Password"),e.qZA()(),e.YNc(8,D,2,1,"ul",7),e.TgZ(9,"div",8)(10,"form",9),e.NdJ("ngSubmit",function(){return r.onSubmit()}),e.TgZ(11,"div",10)(12,"div",11),e._UZ(13,"span",12)(14,"input",13),e.TgZ(15,"span",14),e.NdJ("click",function(){return r.hideShowPass()}),e.qZA()(),e.YNc(16,V,3,2,"div",15),e.qZA(),e.TgZ(17,"div",10)(18,"div",11),e._UZ(19,"span",16)(20,"input",17),e.TgZ(21,"span",14),e.NdJ("click",function(){return r.hideShowPass()}),e.qZA()(),e.YNc(22,ee,3,2,"div",15),e.qZA(),e.TgZ(23,"button",18),e._uU(24,"Change password"),e.qZA()()()()()()()()),2&i&&(e.xp6(8),e.Q6J("ngIf",r.errors),e.xp6(2),e.Q6J("formGroup",r.resetPasswordForm),e.xp6(2),e.ekj("error",r.submitted&&r.form.password.invalid),e.xp6(2),e.ekj("is-invalid",r.submitted&&r.form.password.invalid),e.Q6J("type",r.type),e.xp6(1),e.Gre("fa ",r.eyeIcon," text-primary"),e.xp6(1),e.Q6J("ngIf",r.submitted&&r.form.password.errors),e.xp6(2),e.ekj("error",r.submitted&&r.form.confirmPassword.invalid),e.xp6(2),e.ekj("is-invalid",r.submitted&&r.form.confirmPassword.invalid),e.Q6J("type",r.type),e.xp6(1),e.Gre("fa ",r.eyeIcon," text-primary"),e.xp6(1),e.Q6J("ngIf",r.submitted&&r.form.confirmPassword.errors))},dependencies:[p.sg,p.O5,n._Y,n.Fj,n.JJ,n.JL,n.sg,n.u],styles:[".outer[_ngcontent-%COMP%]{height:100vh;background:linear-gradient(to top,#c9c9ff 50%,#6f3dbc 90%) no-repeat}.panel-heading[_ngcontent-%COMP%]{text-align:center;margin-bottom:10px}#forget[_ngcontent-%COMP%]{min-width:100px;margin-left:auto;text-decoration:none;cursor:pointer}a[_ngcontent-%COMP%]:hover{text-decoration:none}.form-inline[_ngcontent-%COMP%]   label[_ngcontent-%COMP%]{padding-left:10px;margin:0;cursor:pointer}.btn[_ngcontent-%COMP%]{margin-top:20px;border-radius:15px}.errorMessage[_ngcontent-%COMP%]{color:red;padding-left:10px}.panel[_ngcontent-%COMP%]{min-height:280px;box-shadow:20px 20px 80px #dadada;border-radius:12px;margin-top:100px}.input-field[_ngcontent-%COMP%]{border-radius:5px;padding:5px;display:flex;align-items:center;cursor:pointer;border:1px solid #ddd;color:#4343ff}.input-field.error[_ngcontent-%COMP%]{border:1px solid red;color:red}input[type=text][_ngcontent-%COMP%], input[type=password][_ngcontent-%COMP%]{border:none;outline:none;box-shadow:none;width:100%}.fa-eye-slash.btn[_ngcontent-%COMP%]{border:none;outline:none;box-shadow:none}img[_ngcontent-%COMP%]{width:40px;height:40px;object-fit:cover;border-radius:50%;position:relative}"]})}return t})()},{path:"emailconfirmation",component:(()=>{class t{ngOnInit(){this.confirmEmail()}constructor(o,i){this._authService=o,this._route=i,this.confirmEmail=()=>{this.showError=this.showSuccess=!1,this._authService.confirmEmail({token:this._route.snapshot.queryParams.token,email:this._route.snapshot.queryParams.email}).subscribe({next:v=>this.showSuccess=!0,error:v=>{this.showError=!0,this.errorMessage=v.message}})}}static#e=this.\u0275fac=function(i){return new(i||t)(e.Y36(m.B),e.Y36(l.gz))};static#t=this.\u0275cmp=e.Xpm({type:t,selectors:[["app-email-confirmation"]],decls:10,vars:2,consts:[[1,"outer"],[1,"container"],[1,"row","d-flex","align-items-center","justify-content-center"],[1,"col-lg-5","col-md-7"],[1,"panel","border","bg-white"],[1,"panel-heading"],[1,"py-3","font-weight-bold"],["class","alert alert-danger","role","alert",4,"ngIf"],["class","alert alert-success","role","alert",4,"ngIf"],["role","alert",1,"alert","alert-danger"],["role","alert",1,"alert","alert-success"],[3,"routerLink"]],template:function(i,r){1&i&&(e.TgZ(0,"div",0)(1,"div",1)(2,"div",2)(3,"div",3)(4,"div",4)(5,"div",5)(6,"h1",6),e._uU(7,"Email Confirmation"),e.qZA()(),e.YNc(8,re,2,1,"div",7),e.YNc(9,ne,4,2,"div",8),e.qZA()()()()()),2&i&&(e.xp6(8),e.Q6J("ngIf",r.showError),e.xp6(1),e.Q6J("ngIf",r.showSuccess))},dependencies:[p.O5,l.rH],styles:[".outer[_ngcontent-%COMP%]{height:100vh;background:linear-gradient(to top,#c9c9ff 50%,#6f3dbc 90%) no-repeat}.panel-heading[_ngcontent-%COMP%]{text-align:center;margin-bottom:10px}#forget[_ngcontent-%COMP%]{min-width:100px;margin-left:auto;text-decoration:none;cursor:pointer}a[_ngcontent-%COMP%]:hover{text-decoration:none}.form-inline[_ngcontent-%COMP%]   label[_ngcontent-%COMP%]{padding-left:10px;margin:0;cursor:pointer}.btn[_ngcontent-%COMP%]{margin-top:20px;border-radius:15px}.panel[_ngcontent-%COMP%]{min-height:auto;box-shadow:20px 20px 80px #dadada;border-radius:12px;margin-top:100px}.errorMessage[_ngcontent-%COMP%]{color:red;padding-left:10px}"]})}return t})()}];let se=(()=>{class t{static#e=this.\u0275fac=function(i){return new(i||t)};static#t=this.\u0275mod=e.oAB({type:t});static#r=this.\u0275inj=e.cJS({imports:[l.Bz.forChild(ie),l.Bz]})}return t})();var ae=d(7791),de=d(8081);let le=(()=>{class t{static#e=this.\u0275fac=function(i){return new(i||t)};static#t=this.\u0275mod=e.oAB({type:t});static#r=this.\u0275inj=e.cJS({imports:[p.ez,se,ae.m,de.I,u.IL]})}return t})()}}]);