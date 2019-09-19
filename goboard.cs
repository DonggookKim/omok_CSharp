using System;
using system.IO;
public class GoBoard{
	private int[,] goBoard = new int[21,21];//바둑판을 가장자리를 만들것. 가장자리는 2로 표기. 두는 범위는 1~19
	private int Cury=10, Curx=10,su=0;
	private int flag=9; // 누구차례임?
	public int[] lastP=new int[2];//마지막에 둔수
	private string[] kibo;
	public boolproperCoor(int y,int x){
		if(y>=0 && y<=20 && x>=0 && x<=20){
			return true;
		}
		return false;
	}
	public GoBoard(){
		int i,j;
		this.kibo=new string[400];
		for(i=1;i<20;i++){
			for(j=1;j<=19;j++){
				this.goBoard[i,j]=0;
			}
		}
		for(i=0;i<21;i++){
			this.goBoard[0,i]=2;
			this.goBoard[20,i]=2;
			this.goBoard[i,0]=2;
			this.goBoard[i,20]=2;
		}
	}
	public string whosTurn(){
		if(this.flag==0) return "BLACK";
		else return "WHITE";
	}
	public int getX(){ return CurX; }
	public int getY(){ return CurY; }
	public int getBoard(int y,int x){
		if(y>=0 && y<=20 && x>=0 & x<=20){
			return goBoard[y,x];
		}
		return 2;
	}// 바둑판 (y,x)에 있는 것을 리턴
	
	private string printP(int y,int x){
		if(this.goBoard[y,x]==1){
			return "●";
		}
		if(this.goBoard[y,x]==-1){
			return "○";
		}
		if(this.goBoard[y,x]==2){
			return "  ";
		}
		if(y==1){
			if(x==1){ return "┌"; }
			if(x==19){ return "┐"; }
			if(x>1 && x<19){ return "┬"; }
		}
		if(y==19){
			if(x==1){ return "└"; }
			if(x==19){ return "┘"; }
			if(x>1 && x<19){ return "┴"; }
		}
		if(y>1 && y<19){
			if(x==1){ return "┣"; }
			if(x==19){ return "┤"; }
		}
		return "┼";
	}
	public void Msg(string msg){
		Console.SetCursorPosition(0,22);
		Console.BackgroundColor=ConsoleColor.Black;
		Console.ForegroundColor=ConsoleColor.Gray;
		Console.WriteLine(msg);
		Console.BackgroundColor=ConsoleColor.DarkYellow;
		Console.ForegroundColor=ConsoleColor.Black;
	}
	public void printB(){
		int i,j;
		Console.BackgroundColor=ConsoleColor.DarkYellow;
		Console.ForegroundColor=ConsoleColor.Black;
		for(i=0;i<21;i++){
			for(j=0;j<21;j++){
				putImg(i,j);
			}
			Console.WriteLine();
		}
	}
	private bool basicWrongPlace(int y,int x){
		if(this.getBoard(y,x)==0){
			return false;
		}
		if(su==360){
			rhis.Msg("DRAW");
			return false;
		}
		return true;
	}
	protected virtual bool gameWrongPlace(int y,int x){
		return false;
	}
	private bool WrongPlace(int y,int x){
		if(!this.basicWrongPlace(y,x)){
			if(!this.gameWrongPlace(y,x)){
				return false;
			}
		}
		return true;
	}
	
	public void putStone(int y,int x){
		int pn=0;
		string COLOR="";
		switch(this.flag){
			case 0:COLOR="BLACK";pn=1;break;
			case 1:COLOR="WHITE";pn=-1;break;
		}
		if(!this.WrongPlace(y,x)){
			this.goBoard[y,x]=pn;
			this.kibo[su]=COLOR+" : (" + y + "," + x+")";
			this.su++;
			this.lastP[0]=y;
			this.lastP[1]=x;
		}
	}
	public void printKibo(string dire){File.WriteAllLines(@dire,kibo);}
	protected virtual bool gameEnd(){ return true; }
	private void putImg(int y,int x){
		Console.SetCursorPosition(2*x,y);
		if(printP(y,x)=="○"){
			Console.ForegroundColor=ConsoleColor.Gray;
			Console.Write("●");
			Console.ForegroundColor=ConsoleColor.Black;
		}else Console.Write(printP(y,x));
	}
	private String ArrowMove(){
		ConsoleKeyInfo cki;
		String kinfo;
		
		int y=this.CurY,x=this.CurX;
		cki=Console.ReadKey(true);
		kinfo=cki.Key.ToString();
		
		switch(kinfo){
			case "Enter":
				if(this.WrongPlace(y,x)){
					this.Msg("You cannot play here.\n");
					return "Wrong";
				}
				this.Msg("                                       "0;
				return "Enter";
			case "RightArrow":
				x++;
				break;
			case "LeftArrow":
				x--;
				break;
			case "UpArrow":
				y--;
				break;
			case "DownArrow";
				y++;
				break;
			case "Delete":
				return "Break";
		}
		if(x<1) x=1;
		if(x>19) x=19;
		if(y<1) y=1;
		if(y>19) y=19;
		this.CurY=y; this.CurX=x;
		return "";
	}
	private void printCur(){
		string Trun;
		if(this.flag==0){
			Turn="⊙";
		}else{
			Turn="◎";
		}
		Console.SetCursorPosition(2*this.CurX, this.CurY);
		Console.ForegroundColor=ConsoleColor.Red;
		Console.Write(Turn);
		Console.ForegroundColor=ConsoleColor=Black;
		int i;
		int[,] nei=new int[5,2];
		nei[0,0]=-1;nei[1,0]=1;nei[2,0]=0;nei[3,0]=0;
		nei[0,1]=0;nei[1,1]=0;nei[2,1]=1;nei[3,1]=-1;
		
		for(i=0;i<4;i++){
			this.putImg(this.CurY+nei[i,0],this.CurX+nei[i,1]);
		}
	}
	private void putAI(int[] coor){
		this.putStone(coor[0],coor[1]);
		this.putImg(coor[0],coor[1]);
	}
	public virtual int[] AI(string COLOR){
		int[] result=new int[2];
		int i,j;
		for(i=1;i<20;i++){
			for(j=1;j<20;j++){
				if(!this.WrongPlace(i,j)){
					result[0]=i;result[1]=j;
					break;
				}
			}
		}
		return result;
	}
	public void gameStart(string MODE){
		string input="";
		Console.Clear();
		this.printB();
		if(MODE=="DOUBLE"){
			do{
				do{
					this.printCur();
					input=ArrowMove();
				}while(input!="Enter" && input!="Break");
				
				if(input=="Break"){ Console.ForegroundColor=ConsoleColor.Red; break;}
				
				putStone(this.getY(),this.getX());
				
				if(!this.gameEnd()){
					this.kibo[su]=this.whosTurn()+" WIN in " + su + "turns!");
					if(this.whosTurn()=="BLACK")	Console.ForegroundColor=ConsoleColor.Black;
					if(this.whosTurn()=="WHITE")	Console.ForegroundColor=ConsoleColor.Gray;
					break;
				}
				this.flag=-1*this.flag+1;
			}while(true);
		}
		if(MODE=="AI_BLACK"){
			do{
				this.putAI(this.AI("BLACK"));
				this.flag= -1*this.flag+1;
				if(!this.gameEnd()){
					this.kibo[su]="BLACK WIN in " + su+ "turns!";
					Console.ForegroundColor=ConsoleColor.Black;
					break;
				}
				
				do{
					this.printCur();
					input=ArrowMove();
				}whie(input!="Enter" && input!="Break");
				if(input=="Break"){
					Console.ForegroundColor=ConsoleColor.Red;break;
				}
				
				putStone(this.getY(),this.getX());
				
				this.flage= -1*this.flag+1;
				
				if(!this.gameEnd()){
					this.kibo[su]="WHITE WIN in "+su +" turns!";
					Console.ForegroundColor=ConsoleColor.Gray;
					break;
				}
			}while(true);
		}
		if(MODE=="AI_WHITE"){
			do{				
				do{
					this.printCur();
					input=ArrowMove();
				}while(input!="Enter" && input!="Break");
				if(input=="Break"){
					Console.ForegroundColor=ConsoleColor.Red;break;
				}
				
				putStone(this.getY(),this.getX());
				
				this.flage= -1*this.flag+1;
				
				if(!this.gameEnd()){
					this.kibo[su]="BLACK WIN in " + su+ "turns!";
					Console.ForegroundColor=ConsoleColor.Black;
					break;
				}
				this.putAI(this.AI("BLACK"));
				if(!this.gameEnd()){
					this.kibo[su]="WHITE WIN in "+su +" turns!";
					Console.ForegroundColor=ConsoleColor.Gray;
					break;
				}
				this.flag= -1*this.flag+1;
				if(this.gameEnd()){break;}
			}while(true);
		}
		if(MODE=="AIvsAI"){
			Random a = new Random();
			while(gameEnd()){
				this.putAI(this.AI("BLACK"));
				this.flag=-1*this.flag+1;
				if(!this.gameEnd()){
					this.kibo[su]="BLACK WIN in " + su+ "turns!";
					Console.ForegroundColor=ConsoleColor.Black;
					break;
				}
				this.putAI(this.AI("WHITE"));
				this.flag=-1*this.flag+1;
				if(!this.gameEnd()){
					this.kibo[su]="WHITE WIN in "+su +" turns!";
					Console.ForegroundColor=ConsoleColor.Gray;
					break;
				}
				System.Threading.Tread.Sleep(a.Next(500,2500));
			}
		}
		Console.SetCursorPosition(2*this.lastP[1],this.lastP[0]);
		Console.Write("★");
		Console.BackgroundColor=ConsoleColor.Black;
		Console.ForegroundColor=ConsoleColor.Gry;
		this.printKibo("기보.txt");
	}
}

public class Omok:GoBoard{
	public Omok(){}
	private bool double_empty(int y,int x,int vecy, int vecx){
		if(this.getBoard(y+vecy,x+vecx)==0){
			if(this.getBoard(y+2*vecy,x+2*vecx)!=1){
				return true;
			}
		}
		return false;
	}
	protected override bool gameWrongPlace(int y,int x){
		if(this.whosTurn()=="WHITE"){
			return false;
		}
		int i,ch3=0;
		int[,] vec = new int[8,2];
		vec[0,0]=-1;vec[1,0]=-1;vec[2,0]=0;vec[3,0]=1;
		vec[0,1]=0;vec[1,1]=-1;vec[2,1]=-1;vec[3,1]=-1;
		vec[4,0]=1;vec[5,0]=1;vec[6,0]=0;vec[7,0]=-1;
		vec[4,1]=0;vec[5,1]=1;vec[6,1]=1;vec[7,1]=1;
		for(i=0;i<8;i++){
			if(this.getBoard(y+vec[i,0],x+vec[i,1])==0){
				if(this.getBoard(y+vec[i,0]*2,x+vec[i,1]*2)==1
				&& this.double_empy(y+vec[i,0]*2,x+vec[i,1]*2,vec[i,0],vec[i,1])
				&& this.getBoard(y-vec[i,0],x-vec[i,1])==0){
					ch33++;
				}
				if(this.getBoard(y+vec[i,0]*2,x+vec[i,1]*2)==0
				&& this.getBoard(y+vec[i,0]*3,x_vec[i,1]*3)==1
				&& this.double_empty(y+vec[i,0]*3,x+vec[i,1]*3,vec[i,0],vec[i,1])){
					ch33++;
				}
			}else if(this.getBoard(y+vec[i,0],x+vec[i,1])==0){
				if(this.getBoard(y+vec[i,0]*2,x+vec[i,1]*2)==1
				&& this.getBoard(y+vec[i,0]*3, x+vec[i,1]*3)==1
				&& this.double_empty(y+vec[i,0]*3,x+vec[i,1]*3,vec[i,0],vec[i,1])){
					ch33++;
				}
				else if(this.getBoard(y-vec[i,0],x-vec[i,1])==1
				&& this.getBoard(y-2*vec[i,0],x-2*vec[i,1])==0
				&& this.getBoard(y+vec[i,0],x+vec[i,1])==0
				&& this.getBoard(y+2*vec[i,0],x+2*vec[i,1])==1
				&& this.double_empty(y+vec[i,0]*2,x_vec[i,1]*2,vec[i,0],vec[i,1])){
					ch33++;
				}
			}
		}
		for(i=0;i<4;i++){
			if(this.getBoard(y+vec[i,0],x+vec[i,1])==1
			&& this.double_empty(y+vec[i,0],x+vec[i,1],vec[i,0],vec[i,1])
			&& this.getBoard(y-vec[i,0],x-vec[i,1])==1
			&& this.double_empty(y-vec[i,0],x-vec[i,1],-1*vec[i,0],-1*vec[i,1])){
				ch33++;
			}
		}
		if(ch33==2) return true;
		return false;
	}
	private int countStone(int y,int x,int vecty, int vectx){
		int i;
		int cnt=0;
		for(i=0;;i++){
			if(this.properCoor(y+vecty*i,x+vectx*i)
				&& this.getBoard(y+vecty*i,x+vectx*i)==this.getBaord(y,x)){
					cnt++;
			}else break;
		}
		for(i=1;;i++){
			if(this.properCoor(y-vecty*i,x-vectx*i) &&
			this.getBoard(y-vecty*i,x-vectx*i)==this.getBoard(y,x)){
				cnt++;
			}else break;
		}
		return cnt;
	}
	private bool check_Five(int y,int x){
		int [,] vec=new int[4,2];
		vec[0,0]=-1;vec[1,0]=-1;vec[2,0]=0;vec[3,0]=1;
		vec[0,1]=0;vec[1,1]=-1;vec[2,1]=-1;vec[3,1]=-1;
		int i;
		for(i=0;i<4;i++){
			if(this.countStone(y,x,vec[i,0],vec[i,1])==5){
				return true;
			}
		}
		return false;
	}
	protected override bool gameEnd(){
		int y=this.lastP[0],x=this.lastP[1];
		string winner;
		if(this.check_Five(y,x)){
			if(this.getBoard(y,x)==1) winner="BLACK";
			else winner="WHITE";
			Console.SetCursorPosition(0,22);
			Console.BackgroundColor=ConsoleColor.Black;
			Console.ForegroundColor=ConsoleColor.Gray;
			Console.WriteLine("{0} WIN!",winner);
			console.BackgroundColor=ConsoleColor.DarkYellow;
			Console.ForegroundColor=ConsoleColor.Balck;
			return false;
		}
		return true;
	}
}
public class nAI:Omok{
	
}