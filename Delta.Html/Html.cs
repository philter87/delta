namespace Delta.Html;


public static class Html
{
    public static HtmlTag h6(string? classes = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return new HtmlTag("h6").With("class",classes).With("style",style).With("id",id).With("hidden",hidden).With("onclick",onclick);
    }    public static HtmlTag a(string? classes = null, string? href = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return new HtmlTag("a").With("class",classes).With("href",href).With("style",style).With("id",id).With("hidden",hidden).With("onclick",onclick);
    }    public static HtmlTag script(string? classes = null, string? type = null, string? src = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return new HtmlTag("script").With("class",classes).With("type",type).With("src",src).With("style",style).With("id",id).With("hidden",hidden).With("onclick",onclick);
    }    public static HtmlTag link(string? classes = null, string? href = null, string? rel = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return new HtmlTag("link").With("class",classes).With("href",href).With("rel",rel).With("style",style).With("id",id).With("hidden",hidden).With("onclick",onclick);
    }    public static HtmlTag h1(string? classes = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return new HtmlTag("h1").With("class",classes).With("style",style).With("id",id).With("hidden",hidden).With("onclick",onclick);
    }    public static HtmlTag h5(string? classes = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return new HtmlTag("h5").With("class",classes).With("style",style).With("id",id).With("hidden",hidden).With("onclick",onclick);
    }    public static HtmlTag html(string? classes = null, string? xmlns = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return new HtmlTag("html").With("class",classes).With("xmlns",xmlns).With("style",style).With("id",id).With("hidden",hidden).With("onclick",onclick);
    }    public static HtmlTag label(string? classes = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return new HtmlTag("label").With("class",classes).With("style",style).With("id",id).With("hidden",hidden).With("onclick",onclick);
    }    public static HtmlTag header(string? classes = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return new HtmlTag("header").With("class",classes).With("style",style).With("id",id).With("hidden",hidden).With("onclick",onclick);
    }    public static HtmlTag form(string? classes = null, string? onsubmit = null, string? name = null, string? action = null, string? method = null, string? enctype = null, string? target = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return new HtmlTag("form").With("class",classes).With("onsubmit",onsubmit).With("name",name).With("action",action).With("method",method).With("enctype",enctype).With("target",target).With("style",style).With("id",id).With("hidden",hidden).With("onclick",onclick);
    }    public static HtmlTag p(string? classes = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return new HtmlTag("p").With("class",classes).With("style",style).With("id",id).With("hidden",hidden).With("onclick",onclick);
    }    public static HtmlTag h2(string? classes = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return new HtmlTag("h2").With("class",classes).With("style",style).With("id",id).With("hidden",hidden).With("onclick",onclick);
    }    public static HtmlTag head(string? classes = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return new HtmlTag("head").With("class",classes).With("style",style).With("id",id).With("hidden",hidden).With("onclick",onclick);
    }    public static HtmlTag button(string? classes = null, string? name = null, string? type = null, string? value = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return new HtmlTag("button").With("class",classes).With("name",name).With("type",type).With("value",value).With("style",style).With("id",id).With("hidden",hidden).With("onclick",onclick);
    }    public static HtmlTag body(string? classes = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return new HtmlTag("body").With("class",classes).With("style",style).With("id",id).With("hidden",hidden).With("onclick",onclick);
    }    public static HtmlTag h3(string? classes = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return new HtmlTag("h3").With("class",classes).With("style",style).With("id",id).With("hidden",hidden).With("onclick",onclick);
    }    public static HtmlTag meta(string? classes = null, string? name = null, string? content = null, string? charset = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return new HtmlTag("meta").With("class",classes).With("name",name).With("content",content).With("charset",charset).With("style",style).With("id",id).With("hidden",hidden).With("onclick",onclick);
    }    public static HtmlTag h4(string? classes = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return new HtmlTag("h4").With("class",classes).With("style",style).With("id",id).With("hidden",hidden).With("onclick",onclick);
    }    public static HtmlTag nav(string? classes = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return new HtmlTag("nav").With("class",classes).With("style",style).With("id",id).With("hidden",hidden).With("onclick",onclick);
    }    public static HtmlTag span(string? classes = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return new HtmlTag("span").With("class",classes).With("style",style).With("id",id).With("hidden",hidden).With("onclick",onclick);
    }    public static HtmlTag div(string? classes = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return new HtmlTag("div").With("class",classes).With("style",style).With("id",id).With("hidden",hidden).With("onclick",onclick);
    }    public static HtmlTag input(string? classes = null, string? type = null, string? name = null, string? oninput = null, string? value = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return new HtmlTag("input").With("class",classes).With("type",type).With("name",name).With("oninput",oninput).With("value",value).With("style",style).With("id",id).With("hidden",hidden).With("onclick",onclick);
    }
}