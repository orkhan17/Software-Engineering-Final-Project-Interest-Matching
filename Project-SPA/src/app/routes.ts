import {Routes} from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MemberComponent} from './member/member.component';
import { MessageComponent} from './message/message.component';
import { AuthGuard } from './_guards/auth.guard';
import { PreferenceComponent } from './preference/preference.component';
import { DeleteProfileComponent } from './delete-profile/delete-profile.component';
import { PostComponent } from './post/post.component';
import { EditProfileComponent } from './edit-profile/edit-profile.component';
import { SharePostComponent } from './share-post/share-post.component';
import { MypostsComponent } from './myposts/myposts.component';
import { AccountComponent } from './account/account.component';
import { FollowersComponent } from './followers/followers.component';
import { UserpostsComponent } from './userposts/userposts.component';

export const appRoutes: Routes = [
    {path: '', component: HomeComponent},
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [
            {path: 'member', component: MemberComponent},
            {path: 'share-post', component: SharePostComponent},
            {path: 'myposts', component: MypostsComponent},
            {path: 'account', component: AccountComponent},
            {path: 'edit_profile', component: EditProfileComponent},
            {path: 'post', component: PostComponent},
            {path: 'message', component: MessageComponent},
            {path: 'preference', component: PreferenceComponent},
            {path: 'delete_profile', component: DeleteProfileComponent},
            {path: 'following', component: FollowersComponent},
            {path: 'account/:id', component: UserpostsComponent},
        ]

    },
    {path: '**', redirectTo: '', pathMatch: 'full'}
];
