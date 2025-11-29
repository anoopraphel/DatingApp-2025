import { HttpClient} from '@angular/common/http';
import { Component, inject, Injectable } from '@angular/core';
import { environment } from '../environments/environment';
import { Member, Photo } from '../types/member';
import { AccountService } from '../core/services/account-service';

@Component({
  selector: 'app-member-service',
  imports: [],
  templateUrl: './member-service.html',
  styleUrl: './member-service.css'
})
@Injectable({
  providedIn:'root'
})
export class MemberService {
  private http = inject(HttpClient);
  private accountService = inject(AccountService);
  private baseUrl = environment.apiUrl;


  getMembers() {
    return this.http.get<Member[]>(this.baseUrl + 'members');
  }
  getMember(id: string) {
    return this.http.get<Member>(this.baseUrl + 'members/' + id);
  }
  getMemberPhotos(id: string) {
    return this.http.get<Photo[]>(this.baseUrl + 'members/' + id + '/photos');
  }
  
}
