import { Injectable } from '@angular/core';
import { Member } from './member.model';
import { HttpClient } from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class MemberService {

  constructor(private http: HttpClient) {
  }

  readonly _baseUrl = "https://localhost:44312/api/member";
  formData: Member = new Member();
  list: Member[];

  postMember() {
    return this.http.post(this._baseUrl, this.formData);
  }
  putMember() {
    
    return this.http.put(this._baseUrl +'/' + this.formData.id, this.formData);
  }

  deleteMember(id: number) {
    return this.http.delete( this._baseUrl + '/' + id );
  }

  calculatePremium(data: Member) {
    var params = {
      Occupation: this.formData.occupation,
      OccupationId: this.formData.occupationId,
      Age: this.formData.age,
      DeathInsuredSum: this.formData.deathInsuredSum
    };
    return this.http.post(this._baseUrl + '/deathpremiumcalculate', params);
  }

  refreshList() {
    this.http.get(this._baseUrl + '/list')
      .toPromise()
      .then(res => this.list = res as Member[]);
  }
}
