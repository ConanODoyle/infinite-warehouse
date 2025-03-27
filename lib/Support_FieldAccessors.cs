

function simObject::getObjField(%this, %fieldName)
{
	%firstLetter = getSubStr(%fieldName, 0, 1);
	%restLetters = getSubStr(%fieldName, 1, 256);

	switch$(%firstLetter)
	{
		case "a": return %this.a[%restLetters];
		case "b": return %this.b[%restLetters];
		case "c": return %this.c[%restLetters];
		case "d": return %this.d[%restLetters];
		case "e": return %this.e[%restLetters];
		case "f": return %this.f[%restLetters];
		case "g": return %this.g[%restLetters];
		case "h": return %this.h[%restLetters];
		case "i": return %this.i[%restLetters];
		case "j": return %this.j[%restLetters];
		case "k": return %this.k[%restLetters];
		case "l": return %this.l[%restLetters];
		case "m": return %this.m[%restLetters];
		case "n": return %this.n[%restLetters];
		case "o": return %this.o[%restLetters];
		case "p": return %this.p[%restLetters];
		case "q": return %this.q[%restLetters];
		case "r": return %this.r[%restLetters];
		case "s": return %this.s[%restLetters];
		case "t": return %this.t[%restLetters];
		case "u": return %this.u[%restLetters];
		case "v": return %this.v[%restLetters];
		case "w": return %this.w[%restLetters];
		case "x": return %this.x[%restLetters];
		case "y": return %this.y[%restLetters];
		case "z": return %this.z[%restLetters];
		case "_": return %this._[%restLetters];
	}
}

function simObject::setObjField(%this, %fieldName, %newValue)
{
	%firstLetter = getSubStr(%fieldName, 0, 1);
	%restLetters = getSubStr(%fieldName, 1, 256);

	switch$(%firstLetter)
	{
		case "a": %this.a[%restLetters] = %newValue;
		case "b": %this.b[%restLetters] = %newValue;
		case "c": %this.c[%restLetters] = %newValue;
		case "d": %this.d[%restLetters] = %newValue;
		case "e": %this.e[%restLetters] = %newValue;
		case "f": %this.f[%restLetters] = %newValue;
		case "g": %this.g[%restLetters] = %newValue;
		case "h": %this.h[%restLetters] = %newValue;
		case "i": %this.i[%restLetters] = %newValue;
		case "j": %this.j[%restLetters] = %newValue;
		case "k": %this.k[%restLetters] = %newValue;
		case "l": %this.l[%restLetters] = %newValue;
		case "m": %this.m[%restLetters] = %newValue;
		case "n": %this.n[%restLetters] = %newValue;
		case "o": %this.o[%restLetters] = %newValue;
		case "p": %this.p[%restLetters] = %newValue;
		case "q": %this.q[%restLetters] = %newValue;
		case "r": %this.r[%restLetters] = %newValue;
		case "s": %this.s[%restLetters] = %newValue;
		case "t": %this.t[%restLetters] = %newValue;
		case "u": %this.u[%restLetters] = %newValue;
		case "v": %this.v[%restLetters] = %newValue;
		case "w": %this.w[%restLetters] = %newValue;
		case "x": %this.x[%restLetters] = %newValue;
		case "y": %this.y[%restLetters] = %newValue;
		case "z": %this.z[%restLetters] = %newValue;
		case "_": %this._[%restLetters] = %newValue;
	}
}