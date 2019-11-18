import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { appRoutes } from './routes';

import { AppComponent } from './app.component';
import { ErrorInterceptorProvider } from './_services/error.interceptor';
import { AuthService } from './_services/auth.service';
import { NavComponent } from './nav/nav.component';
import {FormsModule} from '@angular/forms';
import { AlertifyService } from './_services/alertify.service';
import { RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MemberComponent } from './member/member.component';
import { MessageComponent } from './message/message.component';
import { AuthGuard } from './_guards/auth.guard';
import { RegisterComponent } from './register/register.component';
import { PreferenceComponent } from './preference/preference.component';
import { AccountService } from './_services/account.service';
import { DeleteProfileComponent } from './delete-profile/delete-profile.component';
import { PostComponent } from './post/post.component';
import { EditProfileComponent } from './edit-profile/edit-profile.component';
import { SharePostComponent } from './share-post/share-post.component';
import { MypostsComponent } from './myposts/myposts.component';
import { TimeAgoPipe } from 'time-ago-pipe';
@NgModule({
   declarations: [
      AppComponent,
      NavComponent,
      HomeComponent,
      MemberComponent,
      MessageComponent,
      RegisterComponent,
      PreferenceComponent,
      DeleteProfileComponent,
      PostComponent,
      EditProfileComponent,
      SharePostComponent,
      MypostsComponent,
      TimeAgoPipe
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      FormsModule,
      RouterModule.forRoot(appRoutes)
   ],
   providers: [
      AuthService,
      AccountService,
      ErrorInterceptorProvider,
      AlertifyService,
      AuthGuard
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
